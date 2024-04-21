using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class GameObjectLocalized
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Language))]
        public int LanguageId { get; set; }
        public Language Language { get; set; }

        [ForeignKey(nameof(GameObject))]
        public int GameObjectId { get; set; }
        public GameObject GameObject { get; set; }
        
        public string Name { get; set; }

    }
}
