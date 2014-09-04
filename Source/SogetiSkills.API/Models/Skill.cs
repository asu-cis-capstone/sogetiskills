using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SogetiSkills.API.Models
{
    public class Skill
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual ICollection<SkillCategory> Categorites { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
    }
}