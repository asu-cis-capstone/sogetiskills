using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.UI.ViewModels.Profile.EditContactInfo
{
    public interface IEditContactInfoViewModelBuilder
    {
        Task<EditContactInfoViewModel> BuildAsync(int userId);
    }
}
