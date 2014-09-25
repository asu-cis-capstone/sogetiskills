using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Managers
{
    public interface ITagManager
    {
        Task<IEnumerable<Tag>> LoadTagsForConsultantAsync(int consultantId);
    }
}
