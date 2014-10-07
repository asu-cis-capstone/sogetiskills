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
        /// <summary>
        /// The id of the resume.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The id of the user that owns the resume.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The actual file contents of the resume.
        /// </summary>
        public byte[] FileData { get; set; }

        /// <summary>
        /// Metadata about the resume file name and type.
        /// </summary>
        public ResumeMetadata Metadata { get; set; }
    }
}
