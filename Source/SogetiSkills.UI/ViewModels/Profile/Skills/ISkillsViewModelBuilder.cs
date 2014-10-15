using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SogetiSkills.UI.ViewModels.Profile.Skills
{
    public interface ISkillsViewModelBuilder
    {
        Task<SkillsViewModel> BuildAsync(int consultantId);
    }
}