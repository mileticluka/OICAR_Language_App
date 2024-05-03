using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class GameFillBlankDTO : GameDTO
    {
        public LanguageDTO Language { get; set; }
        public ContextImageDTO ContextImage { get; set; }
        public string Sentence { get; set; }
    }
}
