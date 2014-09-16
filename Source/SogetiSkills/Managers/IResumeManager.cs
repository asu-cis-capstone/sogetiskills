using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Managers
{
    public interface IResumeManager
    {
        Task<ResumeMetadata> LoadResumeMetadataAsync(int resumeId);
        Task<Resume> LoadResumeById(int resumeId);
        Task UploadResumeAsync(int userId, string fileName, string mimeType, byte[] fileData);
    }
}
