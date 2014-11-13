using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Models
{
    /// <summary>
    /// A consultant that may be on "the beach" and have skills.
    /// </summary>
    public class Consultant : User
    {
        /// <summary>
        /// Gets or sets a flag indicating whether or not the consultant is currently on the beach.
        /// </summary>
        public bool IsOnBeach { get; set; }
    }
}