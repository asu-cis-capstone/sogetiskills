using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Managers
{
    public interface ITagManager
    {
        Task<IEnumerable<Tag>> LoadTagsForConsultant(int consultantId);
    }
}
