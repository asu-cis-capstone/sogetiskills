using SogetiSkills.Core.Managers;
using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SogetiSkills.UI.ViewModels.Profile.Skills
{
    public class SkillsViewModelBuilder : ISkillsViewModelBuilder
    {
        private readonly IUserManager _userManger;
        private readonly ISkillManager _skillManager;

        public SkillsViewModelBuilder(IUserManager userManager, ISkillManager skillManager)
        {
            _userManger = userManager;
            _skillManager = skillManager;
        }

        public async Task<SkillsViewModel> BuildAsync(int consultantId)
        {
            var consultant = (Consultant)(await _userManger.LoadUserByIdAsync(consultantId));

            SkillsViewModel model = new SkillsViewModel();
            model.ConsultantId = consultantId;
            model.ConsultantName = string.Format("{0} {1}", consultant.FirstName, consultant.LastName);
            model.ConsultantSkills = await _skillManager.LoadSkillsForConsultantAsync(consultantId);
            model.CanonicalSkillNames = (await _skillManager.LoadCanonicalSkillsAsync()).Select(x => x.Name).ToList();
            model.ProficiencyLevels = await _skillManager.LoadProficiencyLevelsAsync();

            // Select the level closest to the average of the levels so that we can
            // have it selected by default on the UI.
            var proficiencyLevelClosestToAverage = (from x in model.ProficiencyLevels
                                                    let averageLevel = model.ProficiencyLevels.Average(l => l.Level)
                                                    orderby Math.Abs(x.Level - averageLevel)
                                                    select x).First();
            model.ProficiencyLevelOptions = (from x in model.ProficiencyLevels
                                             select new SelectListItem
                                             {
                                                 Selected = x == proficiencyLevelClosestToAverage,
                                                 Text = x.ToString(),
                                                 Value = x.Level.ToString()
                                             }).ToList();

            return model;
        }
    }
}