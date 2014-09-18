using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SogetiSkills.Managers
{
    public class ResumeManager : IResumeManager
    {
        private readonly SogetiSkillsDataContext _db;

        public ResumeManager(SogetiSkillsDataContext db)
        {
            _db = db;
        }

        public async Task<ResumeMetadata> LoadResumeMetadataAsync(int resumeId)
        {
            return await (from x in _db.Resumes
                          where x.Id == resumeId
                          select x.Metadata).FirstOrDefaultAsync();
        }

        public async Task<Resume> LoadResumeById(int resumeId)
        {
            return await _db.Resumes.FindAsync(resumeId);
        }

        public async Task UploadResumeAsync(int userId, string fileName, string mimeType, byte[] fileData)
        {
            var consultant = await _db.Users.OfType<Consultant>().FirstAsync(x => x.Id == userId);
            if (consultant.ResumeId.HasValue)
            {
                _db.Resumes.RemoveRange(_db.Resumes.Where(x => x.Id == consultant.ResumeId.Value));
            }
            consultant.Resume = new Resume
            {
                Metadata = new ResumeMetadata
                {
                    FileName = fileName,
                    MimeType = mimeType
                },
                FileData = fileData
            };
            await _db.SaveChangesAsync();
        }
    }
}
