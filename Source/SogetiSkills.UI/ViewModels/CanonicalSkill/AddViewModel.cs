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
        public string Keyword { get; set; }
        public string SkillDescription { get; set; }
    }

    public class AddViewModelValidator : AbstractValidator<AddViewModel>
    {
        private readonly ISkillManager _tagManager;

        public AddViewModelValidator(ISkillManager tagManager)
        {
            _tagManager = tagManager;

            RuleFor(x => x.Keyword)
                .NotEmpty().WithMessage("Keyword is required.")
                .Length(1, 450).WithMessage("Keyword must be fewer than 450 characters.")
                .Must(NotAlreadyExist).WithMessage("There is already a tag with keyword \"{0}\".", x => x.Keyword);

        }

        public bool NotAlreadyExist(string keyword)
        {
            var existingTag = _tagManager.LoadByName(keyword);
            if (existingTag == null)
            {
                return true;
            }
            else if (existingTag.IsCanonical == false)
            {
                return true;
            }

            return false;
        }
    }
}