using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Models
{
    /// <summary>
    /// Metadata for a resume uploaded by a consultant.
    /// </summary>
    /// <remarks>
    /// Often we just want to know the file name and mime type of a consultant's resume so that w
    /// can display a link.  By only returning these two fields we can display the link without having
    /// to pull the full binary contents of the resume back from the database.
    /// </remarks>
    public class ResumeMetadata
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
    }
}
