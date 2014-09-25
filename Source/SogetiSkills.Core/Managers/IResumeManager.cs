using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Managers
{
    public interface IResumeManager
    {
        Task<ResumeMetadata> LoadResumeMetadataByUserIdAsync(int userId);
        Task<Resume> LoadResumeByUserId(int userId);
        Task UploadResumeAsync(int userId, string fileName, string mimeType, byte[] fileData);
    }
}
