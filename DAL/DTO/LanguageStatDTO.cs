using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class LanguageStatDTO
    {
        public string StatName { get; set; }
        public int Score { get; set; }
    }
}
