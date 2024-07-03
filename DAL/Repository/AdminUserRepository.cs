using DAL.DTO;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class AdminUserRepository : IAdminUserRepository
    {
        private readonly DataContext ctx;

        public AdminUserRepository(DataContext context)
        {
            ctx = context;
        }

        public AdminUser? GetUser(string username)
        {
            return ctx.AdminUser.FirstOrDefault(adminUser => adminUser.Username.Equals(username));
        }


        public bool Authenticate(LoginDTO login)
        {
            var target = GetUser(login.Username);

            if (target == null)
            {
                return false;
            }

            byte[] salt = Convert.FromBase64String(target.PasswordSalt);
            byte[] hash = Convert.FromBase64String(target.PasswordHash);

            byte[] calcHash = GetHash(login.Password, salt);

            return hash.SequenceEqual(calcHash);
        }

        private byte[] GetHash(string password, byte[] bytesSalt)
        {
            byte[] hash = KeyDerivation.Pbkdf2(
                    password: password,
                    salt: bytesSalt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8);

            return hash;
        }

        public void CreateUser(string username, string password)
        {
            string hash;
            string salt;
            ComputeHashAndSalt(password, out salt, out hash);

            
            ctx.AdminUser.Add(new AdminUser()
            {
                 Username = username,
                 PasswordHash = hash,
                 PasswordSalt = salt
            });
            ctx.SaveChanges();
        }

        private void ComputeHashAndSalt(string password, out string salt, out string hash)
        {
            byte[] bytesSalt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            string b64Salt = Convert.ToBase64String(bytesSalt);

            byte[] bytesHash = GetHash(password, bytesSalt);
            string b64Hash = Convert.ToBase64String(bytesHash);

            salt = b64Salt;
            hash = b64Hash;
        }

    }
}
