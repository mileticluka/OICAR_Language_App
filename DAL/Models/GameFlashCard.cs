using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class GameFlashCard : Game
    {
        public string Text { get; set; }
        public string Answer { get; set; }
    }
}
