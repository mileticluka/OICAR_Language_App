using AutoMapper;
using DAL.DTO;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/game/flash-card")]
    [ApiController]
    public class GameFlashCardController : ControllerBase
    {
        private readonly IGameRepository gameRepository;
        private readonly ILanguageRepository languageRepository;
        private readonly IMapper mapper;
        private readonly Random random;

        public GameFlashCardController(IGameRepository gameRepository, ILanguageRepository languageRepository, IMapper mapper)
        {
            this.gameRepository = gameRepository;
            this.languageRepository = languageRepository;
            this.mapper = mapper;
        }

        [HttpGet("{langId}")]
        public ActionResult<GameFlashCardDTO> GetGame(int langId)
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

            GameFlashCard game = gameRepository.GetRandomGame<GameFlashCard>(language);

            GameFlashCardDTO dto = mapper.Map<GameFlashCardDTO>(game);

            gameRepository.StartGame(intUserId, game);

            return Ok(dto);

        error:
            return (StatusCode(400, ModelState));
        }

        [HttpPost()]
        public ActionResult<GameFillBlankDTO> RespondFlashCard([FromBody] GameSentenceResponseDTO response)
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

            GameFlashCard savedState = gameRepository.GetGame<GameFlashCard>(intUserId);

            if (savedState.Answer.ToLower().Equals(response.Sentence.ToLower()))
            {
                gameRepository.EndGame(intUserId);
                return Ok();
            }

            // User got it wrong
            return (StatusCode(StatusCodes.Status406NotAcceptable));
        error:
            return (StatusCode(400, ModelState));
        }

        [HttpDelete()]
        public ActionResult<GameFlashCard> DeleteGame()
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

            GameFlashCardDTO dto = mapper.Map<GameFlashCardDTO>(gameRepository.GetGame<GameFlashCard>(intUserId));
            gameRepository.EndGame(intUserId);
            return Ok(dto);
        error:
            return (StatusCode(400, ModelState));
        }

    }


}
