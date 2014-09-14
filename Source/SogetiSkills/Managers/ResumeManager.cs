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
    }
}
