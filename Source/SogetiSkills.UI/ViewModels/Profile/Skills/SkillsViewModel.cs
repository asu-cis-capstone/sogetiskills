using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SogetiSkills.UI.ViewModels.Profile.Skills
{
    public class SkillsViewModel
    {
        public int ConsultantId { get; set; }
        public string ConsultantName { get; set; }
        public IEnumerable<ConsultantSkill> ConsultantSkills { get; set; }
        public IEnumerable<string> CanonicalSkillNames { get; set; }
        public IEnumerable<ProficiencyLevel> ProficiencyLevels { get; set; }
        public IEnumerable<SelectListItem> ProficiencyLevelOptions { get; set; }
    }
}