using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IStatsRepository
    {
        public IList<LanguageStat> GetStatsForUser(User user,Language language);
        public LanguageStat? GetStatForUser(User user, Language language, string statName);

        public LanguageStat AddStat(User user, Language language, string statName, int score);
    }
}
