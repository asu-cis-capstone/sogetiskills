using FluentValidation;
using FluentValidation.Attributes;
using SogetiSkills.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SogetiSkills.UI.ViewModels.CanonicalSkill
{
    [Validator(typeof(EditViewModelValidator))]
    public class EditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class EditViewModelValidator : AbstractValidator<EditViewModel>
    {
        private readonly ISkillManager _skillManager;

        public EditViewModelValidator(ISkillManager skillManager)
        {
            _skillManager = skillManager;

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 450).WithMessage("Name must be fewer than 450 characters.")
                .Must(NotAlreadyExist).WithMessage("There is already a skill named \"{0}\".", x => x.Name);
        }

        public bool NotAlreadyExist(EditViewModel model, string name)
        {
            var skill = _skillManager.LoadById(model.Id);
            if (name == skill.Name)
            {
                // The user is not trying to change the skill name.
                return true;
            }
            else
            {
                var existingOtherSkill = _skillManager.LoadByName(name);
                if (existingOtherSkill == null)
                {
                    return true;
                }
                else if (existingOtherSkill.IsCanonical == false)
                {
                    return true;
                }

                return false;
            }
        }
    }
}