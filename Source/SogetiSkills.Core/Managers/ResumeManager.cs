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
    public class ResumeManager : ManagerBase, IResumeManager
    {
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
