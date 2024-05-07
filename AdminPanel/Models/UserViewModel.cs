using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [MinLength(4)]
        [MaxLength(16)]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email addres")]
        public string Email { get; set; }

        [MaxLength(512)]
        public string Bio { get; set; }
        [MaxLength(256)]
        public string ProfilePicturePath { get; set; }
        public bool PublicProfileVisibility { get; set; }
    }
}
