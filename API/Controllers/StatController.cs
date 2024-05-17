using AutoMapper;
using DAL.DTO;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/stats")]
    [ApiController]
    public class StatController : ControllerBase
    {
        private readonly IStatsRepository statsRepository;
        private readonly IUserRepository userRepository;
        private readonly ILanguageRepository languageRepository;
        private readonly IMapper mapper;

        public StatController(IStatsRepository statsRepository, IUserRepository userRepository, ILanguageRepository languageRepository, IMapper mapper)
        {
            this.statsRepository = statsRepository;
            this.userRepository = userRepository;
            this.languageRepository = languageRepository;
            this.mapper = mapper;
        }

        [HttpGet("{langId}")]
        public ActionResult<IList<LanguageStatDTO>> GetStats(int langId)
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

            IList<LanguageStatDTO> stats = mapper.Map<List<LanguageStatDTO>>(statsRepository.GetStats(intUserId, langId));
            return Ok(stats);
        error:
            return (StatusCode(400, ModelState));
        }
    }
}
