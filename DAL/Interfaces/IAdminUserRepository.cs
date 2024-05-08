using DAL.DTO;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IAdminUserRepository
    {
        public AdminUser? GetUser(string username);
        public bool Authenticate(LoginDTO login);
    }
}
