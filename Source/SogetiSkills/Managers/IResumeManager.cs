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
        Task<ResumeMetadata> LoadResumeMetadata(int resumeId);
    }
}
