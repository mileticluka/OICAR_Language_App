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
        private readonly IConfiguration configuration;

        public StatController(IStatsRepository statsRepository, IUserRepository userRepository, ILanguageRepository languageRepository, IMapper mapper, IConfiguration configuration)
        {
            this.statsRepository = statsRepository;
            this.userRepository = userRepository;
            this.languageRepository = languageRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }


        [HttpGet("{languageId}/{statName}")]
        public ActionResult<IList<LanguageStat>> GetStatForUser(int languageId, string statName)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            User? user = userRepository.GetUser(int.Parse(userId));

            if (user == null)
            {
                ModelState.AddModelError("", "User doesn't exist.");
                return (StatusCode(400, ModelState));
            }

            Language? language = languageRepository.GetLanguage(languageId);

            if (language == null)
            {
                ModelState.AddModelError("", "Invalid Language.");
                return (StatusCode(400, ModelState));
            }

            LanguageStat? stat = statsRepository.GetStatForUser(user, language, statName);

            return Ok(mapper.Map<LanguageStatDTO>(stat));
        }

        [HttpGet("{languageId}")]
        public ActionResult<IList<LanguageStat>> GetStatsForUser(int languageId)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            User? user = userRepository.GetUser(int.Parse(userId));

            if (user == null)
            {
                ModelState.AddModelError("", "User doesn't exist.");
                return (StatusCode(400, ModelState));
            }

            Language? language = languageRepository.GetLanguage(languageId);

            if(language == null)
            {
                ModelState.AddModelError("", "Invalid Language.");
                return (StatusCode(400, ModelState));
            }


            IList<LanguageStat> stats = statsRepository.GetStatsForUser(user, language);


            return Ok(mapper.Map<IList<LanguageStatDTO>>(stats));
        }
    }
}
