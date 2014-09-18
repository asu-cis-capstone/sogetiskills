using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SogetiSkills.UI.ViewModels.Profile.Details
{
    public interface IDetailsViewModelBuilder
    {
        Task<DetailsViewModel> BuildAsync(int profileUserId, int loggedInUserId);
    }
}