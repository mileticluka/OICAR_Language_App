using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class GamePickSentence : Game
    {
        public string AnswerSentence { get; set; }
    }
}
