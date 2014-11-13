using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Models
{
    /// <summary>
    /// A consultant that may be on "the beach" along with all of their skills.
    /// </summary>
    public class ConsultantWithSkills : Consultant
    {
        public ConsultantWithSkills()
            : base()
        {
            this.Skills = new List<ConsultantSkill>();
        }

        /// <summary>
        /// The consultant's skills.
        /// </summary>
        public List<ConsultantSkill> Skills { get; set; }
    }
}
