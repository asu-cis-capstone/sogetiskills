using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Models
{
    /// <summary>
    /// A resume uploaded by a consultant.
    /// </summary>
    public class Resume
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] FileData { get; set; }
        public ResumeMetadata Metadata { get; set; }
    }
}
