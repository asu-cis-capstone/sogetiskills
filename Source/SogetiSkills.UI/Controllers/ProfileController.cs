using AttributeRouting.Web.Mvc;
using SogetiSkills.Managers;
using SogetiSkills.Models;
using SogetiSkills.UI.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SogetiSkills.UI.Controllers
{
    [Authorize]
    public partial class ProfileController : ControllerBase
    {
        private readonly IDetailsViewModelBuilder _detailsViewModelBuilder;

        public ProfileController(IDetailsViewModelBuilder detailsViewModelBuilder)
        {
            _detailsViewModelBuilder = detailsViewModelBuilder;
        }

        [GET("Profile/{Id}")]
        public virtual async Task<ActionResult> Details(int id)
        {
            DetailsViewModel model = await _detailsViewModelBuilder.BuildAsync(id, LoggedInUserId.Value);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }
    }
}