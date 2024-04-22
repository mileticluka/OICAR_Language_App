using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class GameFillBlankDTO : GameDTO
    {
        public string Test1 { get; set; }
        public string Test2 { get; set; }
        public string Test3 { get; set; }

        public GameFillBlankDTO() : base("fill-blank") {
        }
    }
}
