using System.Security.Cryptography;
using System.Text;

namespace Intern.Utility
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword))
                return false;
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public static string GenerateTestHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 11);
        }
    }
}
