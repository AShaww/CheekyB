using CheekyModels.Dtos;
using CheekyServices.Constants;
using CheekyServices.Constants.ErrorConstants;
using FluentValidation;

namespace CheekyServices.Validators;
    /// <summary>
    /// Validator for dealing with Skill data entry 
    /// </summary>
    public class SkillValidator : AbstractValidator<SkillModificationDto>
    {
        public SkillValidator()
        {
            RuleSet(RuleSetConstants.PutSkillRuleSetName, () =>
            {
                RuleFor(skill => skill.SkillId).NotNull().NotEmpty().WithMessage(SkillValidationErrors.SkillIdErrorMessage);
            });
            RuleFor(skill => skill.SkillName).NotNull().NotEmpty().WithMessage(SkillValidationErrors.SkillNameErrorMessage);
            RuleFor(skill => skill.SkillTypeId).NotNull().NotEmpty().WithMessage(SkillValidationErrors.SkillIdErrorMessage);
        }
    }
