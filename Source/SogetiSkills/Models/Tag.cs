using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
        public string SkillDescription { get; set; }
        public bool IsCanonical { get; set; }
    }
}
