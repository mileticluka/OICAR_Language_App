using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class GameObject
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey(nameof(GameObjectCategory))]
        public int ObjectCategoryId { get; set; }
        public GameObjectCategory ObjectCategory { get; set; }
    }
}
