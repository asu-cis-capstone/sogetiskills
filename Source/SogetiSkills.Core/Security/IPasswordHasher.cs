using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Security
{
    /// <summary>
    /// Salts and hashes a plaintext password.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Salt and hash the password.
        /// </summary>
        /// <param name="plainTextPassword">The password to salt and hash.</param>
        /// <param name="salt">The salt to use before hashing.  If this is a brand new password then the salt
        /// should be generated with an instance of an ISaltGenerator.</param>
        /// <returns>The salted and hashed password.</returns>
        string Hash(string plainTextPassword, string salt);
    }
}
