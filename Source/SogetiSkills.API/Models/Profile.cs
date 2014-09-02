using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SogetiSkills.API.Models
{
    public class Profile
    {
        public virtual string Username { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Bio { get; set; }
        
        public virtual ICollection<Skill> Skills { get; set; }
    }
}