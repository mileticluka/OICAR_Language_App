using System.ComponentModel;

namespace AdminPanel.Models
{
    public class LoginViewModel
    {
        public class VMLogin
        {
            [DisplayName("User name")]
            public string Username { get; set; }
            [DisplayName("Password")]
            public string Password { get; set; }
        }
    }
}
