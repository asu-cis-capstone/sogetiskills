using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Managers
{
    /// <summary>
    /// Provides data access for consultants' resumes.
    /// </summary>
    public interface IResumeManager
    {
        /// <summary>
        /// Load just the resume metadata for a consultant, if they have one.
        /// </summary>
        /// <param name="userId">The user id of the consultant whose resume we are loading.</param>
        /// <returns>Metadata for the consultant's resume.</returns>
        Task<ResumeMetadata> LoadResumeMetadataByUserIdAsync(int userId);

        /// <summary>
        /// Loads the actual resume (including file contents) for the user.
        /// </summary>
        /// <param name="userId">The user id of the consultant whose resume we are loading.</param>
        /// <returns>The consultant's resume.</returns>
        Task<Resume> LoadResumeByUserIdAsync(int userId);

        /// <summary>
        /// Upload a new resume for a consultant.  If the consultant already had a resume then it is
        /// replaced with this one.
        /// </summary>
        /// <param name="userId">The id of the consultant that owns the resume.</param>
        /// <param name="fileName">The name of the resume file.</param>
        /// <param name="mimeType">The mime type of the resume file.</param>
        /// <param name="fileData">The actual binary contents of the resume file.</param>
        Task UploadResumeAsync(int userId, string fileName, string mimeType, byte[] fileData);
    }
}
