using AutoMapper;
using DAL.DTO;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/")]
    [Authorize]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository gameRepository;
        private readonly ILanguageRepository languageRepository;
        private readonly IMapper mapper;

        public GameController(IGameRepository gameRepository, ILanguageRepository languageRepository, IMapper mapper)
        {
            this.gameRepository = gameRepository;
            this.languageRepository = languageRepository;
            this.mapper = mapper;
        }

        [HttpGet("get-game/{langId}/{type}")]
        public ActionResult<GameDTO> GetGameFillBlank(int langId, string? type)
        {
            if (type == null)
            {
                ModelState.AddModelError("", "Invalid game type");
                goto error;
            }

            Language? language = languageRepository.GetLanguage(langId);
            if (language == null)
            {
                ModelState.AddModelError("", "Language does not exist");
                goto error;
            }

            if (type.Equals("fill-blank"))
            {
                GameFillBlank game = gameRepository.GetRandomGame<GameFillBlank>(language);
                GameFillBlankDTO dto = mapper.Map<GameFillBlankDTO>(game);
                return Ok(dto);
            }
            else if (type.Equals("flash-cards"))
            {
                GameFlashCard game = gameRepository.GetRandomGame<GameFlashCard>(language);
                GameFlashCardDTO dto = mapper.Map<GameFlashCardDTO>(game);
                return Ok(dto);
            } else if (type.Equals("pick-sentence"))
            {
                GamePickSentence game = gameRepository.GetRandomGame<GamePickSentence>(language);
                GamePickSentenceDTO dto = mapper.Map<GamePickSentenceDTO>(game);
                return Ok(dto);
            }

            error:
            return (StatusCode(400, ModelState));
        }

        [HttpPost("respond/{langId}/fill-blank")]
        public ActionResult<GameDTO> RespondFillBlank(GameFillBlankDTO dto)
        {
            if (!dto.GameType.Equals("fill-blank"))
            {
                return BadRequest();
            }



            return Ok(dto);
        }
    }
}
