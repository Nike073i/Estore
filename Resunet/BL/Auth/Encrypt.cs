using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace Estore.BL.Auth
{
    public class Encrypt : IEncrypt
    {
        public string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Encoding.ASCII.GetBytes(salt), 
                KeyDerivationPrf.HMACSHA512, 
                5000,  // итераций
                64
                )); // 512 / 8
        }
    }
}
