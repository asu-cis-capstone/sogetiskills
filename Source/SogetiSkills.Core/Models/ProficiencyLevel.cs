using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Models
{
    /// <summary>
    /// An estimation of a consultant's proficiency in a skill.
    /// </summary>
    public class ProficiencyLevel
    {
        /// <summary>
        /// A numeric value for the proficiency level where higher numbers indicate a greater proficiency.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// A friendly name for the proficiency level.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A more detailed description of the proficiency level written in second person.
        /// </summary>
        public string SecondPersonDescription { get; set; }

        /// <summary>
        /// A more detailed description of the proficiency level written in third person.
        /// </summary>
        public string ThirdPersonDescription { get; set; }

        /// <summary>
        /// Returns a description of the proficiency level as a string in the form "{Level} - {Name}".  For example
        /// "3 - Intermediate".
        /// </summary>
        /// <returns>A description of the proficiency level as a string.</returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", Level, Name);
        }
    }
}
