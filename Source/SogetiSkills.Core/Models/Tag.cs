using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Models
{
    /// <summary>
    /// A tag that has been applied to a consultant.  Represents a skill that the consultant has.
    /// </summary>
    /// <remarks>Canonical tags are those that have been entered by an account executive as part of a master list.
    /// Consultants can enter a free form string for one of their tags but by suggesting that the consultant
    /// picks from a list of canonical tags we hope to eliminate minor variations in the spelling of very 
    /// common skills.
    /// </remarks>
    public class Tag
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
        public string SkillDescription { get; set; }
        public bool IsCanonical { get; set; }
    }
}
