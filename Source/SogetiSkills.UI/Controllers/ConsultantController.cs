using AttributeRouting.Web.Mvc;
using SogetiSkills.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SogetiSkills.UI.Controllers
{
    public partial class ConsultantController : SogetiSkillsControllerBase
    {
        private readonly ISearchManager _searchManager;

        public ConsultantController(ISearchManager searchManager)
        {
            _searchManager = searchManager;
        }

        [GET("Consultants/Search")]
        public virtual async Task<ActionResult> Search(bool? beachStatus, string lastName, string emailAddress, string skills)
        {
            bool anySearchCriteriaSpecified = !string.IsNullOrWhiteSpace(lastName)
                || !string.IsNullOrWhiteSpace(emailAddress)
                || !string.IsNullOrWhiteSpace(skills);

            ViewBag.BeachStatusOptions = new SelectListItem[] 
            {
                new SelectListItem { Text = "Yes", Value = "true" },
                new SelectListItem { Text = "No", Value = null }
            };

            if (anySearchCriteriaSpecified)
            {
                IEnumerable<string> skillsList = Enumerable.Empty<string>();
                if (!string.IsNullOrWhiteSpace(skills))
                {
                    skillsList = skills.Split(',');
                }
                var searchResults = await _searchManager.SearchConsultantsAsync(beachStatus, lastName, emailAddress, skillsList, SkillSearchType.ConstultantMustHaveAtLeastOneMatchingSkill);
                return View(searchResults);
            }
            else
            {
                return View();
            }
        }
    }
}