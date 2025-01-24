using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace BlogApp.Services
{
    public class PasswordService
    {
        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "Lösenord får inte vara null eller tomt.");

            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string plainTextPassword, string hashedPassword)
        {
            if (string.IsNullOrEmpty(plainTextPassword) || string.IsNullOrEmpty(hashedPassword))
            {
                return false; //Felaktiga indata
            }

            return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
        }
    }
}

 