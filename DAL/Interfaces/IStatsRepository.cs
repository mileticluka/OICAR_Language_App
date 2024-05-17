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
        public IList<LanguageStat> GetStats(int userId, int languageId);
        public LanguageStat AddStat(User user, Language language, string statName, int score);
    }
}
