using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Security
{
    /// <summary>
    /// Generates a salt string to be used when salting and hashing a password.
    /// </summary>
    public class SaltGenerator : ISaltGenerator
    {
        /// <summary>
        /// Generate a new salt string.
        /// </summary>
        /// <returns>The new salt string.</returns>
        public string GenerateNewSalt()
        {
            // Salt for passwords doesn't have to be truly random, it just has to be unique so a
            // GUID will work fine.
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
