using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User
    {
        public int Id { get; set; }

        [MinLength(4)]
        [MaxLength(16)]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email addres")]
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }


        [MaxLength(512)]
        public string Bio { get; set; }
        [MaxLength(256)]
        public string ProfilePicturePath { get; set; }
        public bool PublicProfileVisibility { get; set; }
    }
}
