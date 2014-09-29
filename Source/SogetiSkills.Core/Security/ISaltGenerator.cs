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
    public interface ISaltGenerator
    {
        /// <summary>
        /// Generate a new salt string.
        /// </summary>
        /// <returns>The new salt string.</returns>
        string GenerateNewSalt();
    }
}
