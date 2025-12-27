using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BookAuthorApi.Auth
{
    public static class UserStore
    {
        // Pre-cargados para demo. Password: "Password123"
        public static List<User> Users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                PasswordHash = HashPassword("Password123")
            },
            new User
            {
                Id = Guid.NewGuid(),
                Username = "user",
                PasswordHash = HashPassword("Password123")
            }
        };

        public static User? ValidateUser(string username, string password)
        {
            var hash = HashPassword(password);
            return Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hash);
        }

        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
