using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Security
{
    /// <summary>
    /// Salts and hashes a plaintext password.
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// Salt and hash the password.
        /// </summary>
        /// <param name="plainTextPassword">The password to salt and hash.</param>
        /// <param name="salt">The salt to use before hashing.  If this is a brand new password then the salt
        /// should be generated with an instance of an ISaltGenerator.</param>
        /// <returns>The salted and hashed password.</returns>
        public string Hash(string plainTextPassword, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            // Note that Rfc2898DeriveBytes is a managed implementation of PBKDF2.
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(plainTextPassword, saltBytes))
            {
                byte[] hashBytes = pbkdf2.GetBytes(128);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
