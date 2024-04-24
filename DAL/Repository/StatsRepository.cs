using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repository
{
    public class StatsRepository : IStatsRepository
    {
        private readonly DataContext ctx;

        public StatsRepository(DataContext context)
        {
            ctx = context;
        }

        public LanguageStat AddStat(User user, Language language, string statName,int score)
        {
            LanguageStat? newStat = GetStatForUser(user, language, statName);

            if (newStat == null)
            {
                newStat = new LanguageStat()
                {
                    StatName = statName,

                    LanguageId = language.Id,
                    Language = language,

                    User = user,
                    UserId = user.Id,
                };

                ctx.LanguageStat.Add(newStat);
            }

            newStat.Score += score;

            ctx.SaveChanges();

            return newStat;
        }

        public LanguageStat? GetStatForUser(User user, Language language, string statName)
        {
            LanguageStat? stat =  ctx.LanguageStat.FirstOrDefault(ls => ls.User == user && ls.Language == language && ls.StatName == statName);

            if (stat == null)
            {
                stat = new LanguageStat()
                {
                    User = user,
                    UserId = user.Id,

                    LanguageId = language.Id,
                    Language = language,

                    StatName = statName,

                    Score = 0
                };
            }

            return stat; 
        }

        public IList<LanguageStat> GetStatsForUser(User user, Language language)
        {
            IList<LanguageStat> stats = ctx.LanguageStat.Where(ls => ls.UserId == user.Id && ls.Language == language).ToList();

            return stats;
        }


    }
}
