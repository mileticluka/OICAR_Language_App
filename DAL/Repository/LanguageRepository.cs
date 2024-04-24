using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly DataContext ctx;

        public LanguageRepository(DataContext context)
        {
            ctx = context;
        }

        public IList<Language> GetAllLanguages()
        {
            return ctx.Language.ToList();
        }

        public Language? GetLanguage(int id)
        {
            return ctx.Language.FirstOrDefault(l => l.Id == id);
        }
    }
}
