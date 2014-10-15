using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SogetiSkills.UI.ViewModels.Profile.Skills
{
    public class SkillsViewModel
    {
        public int ConsultantId { get; set; }
        public string ConsultantName { get; set; }
        public IEnumerable<Skill> ConsultantSkills { get; set; }
        public IEnumerable<string> CanonicalSkillNames { get; set; }
    }
}