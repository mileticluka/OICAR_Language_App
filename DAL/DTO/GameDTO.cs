using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class GameDTO
    {
        public int Id { get; set; }
        public LanguageDTO Language { get; set; }
        public ContextImageDTO ContextImage { get; set; }
    }
}
