using DAL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        public User? GetUser(int id);
        public User? GetUser(string username);

        public bool UserExists(int id);
        public bool UserExists(string username);

        public bool Authenticate(LoginDTO login);
        public bool CanCreate(RegisterDTO register);
        public void Register(RegisterDTO register);
    }
}
