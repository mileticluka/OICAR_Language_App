using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class GamePickSentence
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Language))]
        public int LanguageId { get; set; }
        public Language Language { get; set; }

        [ForeignKey(nameof(ContextImage))]
        public int ContextImageId { get; set; }
        public ContextImage ContextImage { get; set; }

        public string AnswerSentence { get; set; }
    }
}
