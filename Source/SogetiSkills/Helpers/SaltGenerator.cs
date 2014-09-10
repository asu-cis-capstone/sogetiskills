using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Helpers
{
    public class SaltGenerator
    {
        public virtual byte[] Generate()
        {
            // Salt for passwords doesn't have to be truly random, it just has to be unique so a
            // guid will work fine.
            return Guid.NewGuid().ToByteArray();
        }
    }
}
