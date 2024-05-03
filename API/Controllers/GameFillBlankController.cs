using AutoMapper;
using DAL.DTO;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/game/fill-blank")]
    [ApiController]
    public class GameFillBlankController : ControllerBase
    {
        private readonly IGameRepository gameRepository;
        private readonly ILanguageRepository languageRepository;
        private readonly IMapper mapper;
        private readonly Random random;

        public GameFillBlankController(IGameRepository gameRepository, ILanguageRepository languageRepository, IMapper mapper)
        {
            this.gameRepository = gameRepository;
            this.languageRepository = languageRepository;
            this.mapper = mapper;
            this.random = new Random();
        }

        [HttpGet("{langId}")]
        public ActionResult<GameFillBlankDTO> GetGame(int langId)
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

            if (gameRepository.IsPlaying(intUserId))
            {
                ModelState.AddModelError("", "User is already playing another game. Please end it before starting a new one");
                goto error;
            }

            GameFillBlank game = gameRepository.GetRandomGame<GameFillBlank>(language);
            string[] words = game.Sentence.Split(" ");
            words[random.Next(words.Length)] = "_";

            GameFillBlankDTO dto = mapper.Map<GameFillBlankDTO>(game);
            dto.Sentence = string.Join(" ", words);

            gameRepository.StartGame(intUserId, game);

            return Ok(dto);

        error:
            return (StatusCode(400, ModelState));
        }

        [HttpPost()]
        public ActionResult<GameFillBlankDTO> RespondFillBlank([FromBody] GameFillBlankDTO dto)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                ModelState.AddModelError("", "Invalid token. Please login again.");
                goto error;
            }
            int intUserId = int.Parse(userId);

            if (!gameRepository.IsPlaying(intUserId))
            {
                ModelState.AddModelError("", "User is not playing any game. Start one before responding.");
                goto error;
            }

            GameFillBlank savedState = gameRepository.GetGame<GameFillBlank>(intUserId);
            if (savedState.Sentence.ToLower().Equals(dto.Sentence.ToLower()))
            {
                gameRepository.EndGame(intUserId);
                return Ok(dto);
            }

            // User got it wrong
            return (StatusCode(StatusCodes.Status406NotAcceptable, dto));
        error:
            return (StatusCode(400, ModelState));
        }

        [HttpDelete()]
        public ActionResult<GameFillBlankDTO> DeleteGame()
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

            gameRepository.EndGame(intUserId);

            return Ok();
        error:
            return (StatusCode(400, ModelState));
        }

    }
}
