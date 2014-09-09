using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Models
{
    public class HashedPassword
    {
        public int Id { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}
