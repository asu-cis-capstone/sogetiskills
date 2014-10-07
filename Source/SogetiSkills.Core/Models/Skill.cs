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
    public class Skill
    {
        /// <summary>
        /// The id of the skill.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the skill.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A more detailed description of the skill.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Whether or not the skill is considered to be canonical.  See class remarks.
        /// </summary>
        public bool IsCanonical { get; set; }
    }
}
