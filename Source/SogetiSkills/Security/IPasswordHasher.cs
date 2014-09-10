using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Security
{
    public interface IPasswordHasher
    {
        string Hash(string plainTextPassword, string salt);
    }
}
