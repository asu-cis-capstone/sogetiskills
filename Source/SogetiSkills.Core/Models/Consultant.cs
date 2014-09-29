using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Models
{
    /// <summary>
    /// A consultant that may be on "the beach" and have tags.
    /// </summary>
    public class Consultant : User
    {
        public bool IsOnBeach { get; set; }
    }
}