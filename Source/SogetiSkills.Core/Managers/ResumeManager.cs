using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SogetiSkills.Core.Helpers;

namespace SogetiSkills.Core.Managers
{
    /// <summary>
    /// Provides data access for consultants' resumes.
    /// </summary>
    public class ResumeManager : ManagerBase, IResumeManager
    {
        /// <summary>
        /// Load just the resume metadata for a consultant, if they have one.
        /// </summary>
        /// <param name="userId">The user id of the consultant whose resume we are loading.</param>
        /// <returns>Metadata for the consultant's resume.</returns>
        public async Task<ResumeMetadata> LoadResumeMetadataByUserIdAsync(int userId)
        {
            var command = new SqlCommand("Resume_SelectMetadataByUserId", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userId", userId);
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while(await reader.ReadAsync())
                {
                    var resumeMetadata = new ResumeMetadata();
                    resumeMetadata.FileName = reader.Field<string>("FileName");
                    resumeMetadata.MimeType = reader.Field<string>("MimeType");

                    return resumeMetadata;
                }
            }
            
            return null;
        }

        /// <summary>
        /// Loads the actual resume (including file contents) for the user.
        /// </summary>
        /// <param name="userId">The user id of the consultant whose resume we are loading.</param>
        /// <returns>The consultant's resume.</returns>
        public async Task<Resume> LoadResumeByUserId(int userId)
        {
            var command = new SqlCommand("Resume_SelectByUserId", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userId", userId);
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var resume = new Resume();
                    resume.Id = reader.Field<int>("Id");
                    resume.UserId = reader.Field<int>("UserId");
                    resume.FileData = reader.Field<byte[]>("FileData");
                    resume.Metadata = new ResumeMetadata();
                    resume.Metadata.FileName = reader.Field<string>("FileName");
                    resume.Metadata.MimeType = reader.Field<string>("MimeType");

                    return resume;
                }
            }
            
            return null;
        }

        /// <summary>
        /// Upload a new resume for a consultant.  If the consultant already had a resume then it is
        /// replaced with this one.
        /// </summary>
        /// <param name="userId">The id of the consultant that owns the resume.</param>
        /// <param name="fileName">The name of the resume file.</param>
        /// <param name="mimeType">The mime type of the resume file.</param>
        /// <param name="fileData">The actual binary contents of the resume file.</param>
        public async Task UploadResumeAsync(int userId, string fileName, string mimeType, byte[] fileData)
        {
            var command = new SqlCommand("Resume_Insert", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@fileName", fileName);
            command.Parameters.AddWithValue("@mimeType", mimeType);
            command.Parameters.AddWithValue("@fileData", fileData);

            await command.ExecuteNonQueryAsync();
        }
    }
}
