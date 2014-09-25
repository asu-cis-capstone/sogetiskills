using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Security
{
    public class SaltGenerator : ISaltGenerator
    {
        public string GenerateNewSalt()
        {
            // Salt for passwords doesn't have to be truly random, it just has to be unique so a
            // guid will work fine.
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
