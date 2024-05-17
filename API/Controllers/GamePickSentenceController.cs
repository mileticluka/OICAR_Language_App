using AutoMapper;
using DAL;
using DAL.DTO;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/game/pick-sentence")]
    [ApiController]
    public class GamePickSentenceController : ControllerBase
    {
        private readonly IGameRepository gameRepository;
        private readonly IUserRepository userRepository;
        private readonly IStatsRepository statsRepository;
        private readonly ILanguageRepository languageRepository;
        private readonly IMapper mapper;
        private readonly Random random;

        public GamePickSentenceController(IGameRepository gameRepository, ILanguageRepository languageRepository, IMapper mapper, IUserRepository userRepository, IStatsRepository statsRepository)
        {
            this.gameRepository = gameRepository;
            this.languageRepository = languageRepository;
            this.mapper = mapper;
            this.random = new Random();
            this.userRepository = userRepository;
            this.statsRepository = statsRepository;
        }

        [HttpGet("{langId}")]
        public ActionResult<GamePickSentenceDTO> GetGame(int langId)
        {
            Language? language = languageRepository.GetLanguage(langId);
            if (language == null)
            {
                ModelState.AddModelError("", "Language does not exist");
                goto error;
            }

            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                ModelState.AddModelError("", "Invalid token. Please login again.");
                goto error;
            }
            int intUserId = int.Parse(userId);
            User? user = userRepository.GetUser(intUserId);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid user. Please login again.");
                goto error;
            }

            if (gameRepository.IsPlaying(intUserId))
            {
                ModelState.AddModelError("", "User is already playing another game. Please end it before starting a new one");
                goto error;
            }

            GamePickSentence game = gameRepository.GetRandomGame<GamePickSentence>(language);
            HashSet<string> answers = new HashSet<string>();
            answers.Add(game.AnswerSentence);
            // We'll get other answers by getting them from other random games
            for (int i = 0; answers.Count < 3 && i < 50 /* max inters */; i++)
            {
                answers.Add(gameRepository.GetRandomGame<GamePickSentence>(language).AnswerSentence);
            }

            GamePickSentenceDTO dto = mapper.Map<GamePickSentenceDTO>(game);
            dto.Answers = (IList<string>) answers.ToList().OrderBy(item => random.Next()).ToList();
 //           dto.AnswerSentence = null;    // for development purposes we will deliver answer sentence
            
            gameRepository.StartGame(intUserId, game);
            statsRepository.AddStat(user, language, Constants.STAT_PICK_SENTENCE_PLAYED, 1);

            return Ok(dto);

        error:
            return (StatusCode(400, ModelState));
        }

        [HttpPost()]
        public ActionResult<GamePickSentenceDTO> RespondFillBlank([FromBody] GameSentenceResponseDTO response)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                ModelState.AddModelError("", "Invalid token. Please login again.");
                goto error;
            }
            int intUserId = int.Parse(userId);
            User? user = userRepository.GetUser(intUserId);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid user. Please login again.");
                goto error;
            }

            if (!gameRepository.IsPlaying(intUserId))
            {
                ModelState.AddModelError("", "User is not playing any game. Start one before responding.");
                goto error;
            }

            GamePickSentence savedState = gameRepository.GetGame<GamePickSentence>(intUserId);
            if (savedState.AnswerSentence.ToLower().Equals(response.Sentence.ToLower()))
            {
                gameRepository.EndGame(intUserId);
                statsRepository.AddStat(user, savedState.Language, Constants.STAT_PICK_SENTENCE_COMPLETED, 1);
                return Ok();
            }

            // User got it wrong
            return (StatusCode(StatusCodes.Status406NotAcceptable));
        error:
            return (StatusCode(400, ModelState));
        }

        [HttpDelete()]
        public ActionResult<GamePickSentenceDTO> DeleteGame()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                ModelState.AddModelError("", "Invalid token. Please login again.");
                goto error;
            }
            int intUserId = int.Parse(userId);

            // we could ignore it, but we won't let the front end devs just spam this so they're always in a safe state.
            // Bandwidth ain't free
            if (!gameRepository.IsPlaying(intUserId))
            {
                ModelState.AddModelError("", "User is not playing any game.");
                goto error;
            }

            GamePickSentenceDTO dto = mapper.Map<GamePickSentenceDTO>(gameRepository.GetGame<GamePickSentence>(intUserId));
            gameRepository.EndGame(intUserId);

            return Ok(dto);
        error:
            return (StatusCode(400, ModelState));
        }

    }
}
