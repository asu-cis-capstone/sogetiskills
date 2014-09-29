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
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
