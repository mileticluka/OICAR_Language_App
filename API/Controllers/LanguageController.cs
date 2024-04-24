using AutoMapper;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/lang")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageRepository languageRepository;
        private readonly IMapper mapper;

        public LanguageController(ILanguageRepository languageRepository, IMapper mapper)
        {
            this.languageRepository = languageRepository;
            this.mapper = mapper;
        }

        [HttpGet()]
        public ActionResult<IList<Language>> GetLanguages()
        {
            return Ok(mapper.Map<IList<Language>>(languageRepository.GetAllLanguages()));
        }

        [HttpGet("{id}")]
        public ActionResult<Language> GetLanguages(int id)
        {
            return Ok(mapper.Map<Language>(languageRepository.GetLanguage(id)));
        }
    }
}
