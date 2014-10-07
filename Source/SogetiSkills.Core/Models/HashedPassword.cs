using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Models
{
    /// <summary>
    /// A hashed and salted password stored in the database.
    /// </summary>
    public class HashedPassword
    {
        /// <summary>
        /// The salted and hashed password.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// The salt to apply to the password.
        /// </summary>
        public string Salt { get; set; }
    }
}
