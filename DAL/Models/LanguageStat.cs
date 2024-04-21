using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class LanguageStat
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Language))]
        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public string StatName { get; set; }
        public int Score { get; set; }
    }
}
