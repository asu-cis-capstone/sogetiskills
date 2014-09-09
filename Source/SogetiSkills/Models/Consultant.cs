using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Models
{
    public class Consultant : User
    {
        public int? ResumeId { get; set; }

        public Resume Resume { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}