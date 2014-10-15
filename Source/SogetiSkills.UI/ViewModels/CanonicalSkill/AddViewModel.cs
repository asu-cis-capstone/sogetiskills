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
    [Validator(typeof(AddViewModelValidator))]
    public class AddViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class AddViewModelValidator : AbstractValidator<AddViewModel>
    {
        private readonly ISkillManager _skillManager;

        public AddViewModelValidator(ISkillManager skillManager)
        {
            _skillManager = skillManager;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 450).WithMessage("Name must be fewer than 450 characters.")
                .Must(NotAlreadyExist).WithMessage("There is already a skill named \"{0}\".", x => x.Name);

        }

        public bool NotAlreadyExist(string name)
        {
            var existingSkill = _skillManager.LoadByName(name);
            if (existingSkill == null)
            {
                return true;
            }
            else if (existingSkill.IsCanonical == false)
            {
                return true;
            }

            return false;
        }
    }
}