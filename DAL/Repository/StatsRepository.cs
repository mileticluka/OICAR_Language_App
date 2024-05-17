using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class StatsRepository : IStatsRepository
    {
        private readonly DataContext ctx;

        public StatsRepository(DataContext context)
        {
            ctx = context;
        }

        public IList<LanguageStat> GetStats(int userId, int languageId)
        {
            var stats = ctx.LanguageStat.Include(ls => ls.User).Include(ls => ls.Language)
                .Where(ls => ls.UserId == userId && ls.LanguageId == languageId).ToList();

            foreach (var item in Constants.LIST_ALL_STATS)
            {
                if (stats.Any(stat => stat.StatName.ToUpper().Equals(item.ToUpper()))) continue;

                stats.Add(new LanguageStat() {
                    Score = 0,
                    StatName = item.ToLower()
                });
            }

            return stats;
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
                    UserId = user.Id,
                    Score = 0
                };

                ctx.LanguageStat.Add(newStat);
            }

            newStat.Score += score;

            ctx.SaveChanges();

            return newStat;
        }

        private LanguageStat? GetStatForUser(User user, Language language, string statName)
        {
            LanguageStat? stat =  ctx.LanguageStat.FirstOrDefault(ls => ls.User == user && ls.Language == language && ls.StatName == statName);

 /*           if (stat == null)
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
            }*/

            return stat; 
        }
    }
}
