using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string plainTextPassword, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(plainTextPassword, saltBytes))
            {
                byte[] hashBytes = pbkdf2.GetBytes(128);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
