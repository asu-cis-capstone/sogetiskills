using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Models
{
    /// <summary>
    /// Many-to-many class representing the relationship between a consultant and a skill that they have.
    /// </summary>
    public class ConsultantSkill
    {
        /// <summary>
        /// The id of the consultant that has the skill.
        /// </summary>
        public int ConsultantId { get; set; }

        /// <summary>
        /// The id of the skill that the consultant has.
        /// </summary>
        public int SkillId { get; set; }

        /// <summary>
        /// The name of the skill.
        /// </summary>
        public string SkillName { get; set; }

        /// <summary>
        /// Whether or not the skill is canonical.
        /// </summary>
        public bool IsCanonical { get; set; }

        /// <summary>
        /// An estimation, by the consultant, of their proficiency in the skill.
        /// </summary>
        public ProficiencyLevel Proficiency { get; set; }
    }
}
