using AutoMapper;
using DAL.DTO;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository gameRepository;
        private readonly IMapper mapper;

        public GameController(IGameRepository gameRepository, IMapper mapper)
        {
            this.gameRepository = gameRepository;
            this.mapper = mapper;
        }

        [HttpGet("get-game/{type}")]
        public ActionResult<GameDTO> GetGameFillBlank(string? type)
        {
            if (type == null)
            {
                goto error;
            }

            if (type.Equals("fill-blank"))
            {
                GameFillBlank game = gameRepository.GetRandomGame<GameFillBlank>();
                GameFillBlankDTO dto = mapper.Map<GameFillBlankDTO>(game);
                return Ok(dto);
            }
            else if (type.Equals("flash-cards"))
            {
                GameFlashCard game = gameRepository.GetRandomGame<GameFlashCard>();
                GameFlashCardDTO dto = mapper.Map<GameFlashCardDTO>(game);
                return Ok(dto);
            } else if (type.Equals("pick-sentence"))
            {
                GamePickSentence game = gameRepository.GetRandomGame<GamePickSentence>();
                GamePickSentenceDTO dto = mapper.Map<GamePickSentenceDTO>(game);
                return Ok(dto);
            }

            error:
            ModelState.AddModelError("", "Invalid game type");
            return (StatusCode(400, ModelState));
        }

        [HttpGet("respond")]
        public ActionResult<GameDTO> Respond(GameDTO dto)
        {
            return Ok(dto);
        }
    }
}
