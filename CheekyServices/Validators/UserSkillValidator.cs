using CheekyModels.Dtos;
using CheekyServices.Constants;
using CheekyServices.Constants.ErrorConstants;
using FluentValidation;

namespace CheekyServices.Validators;

/// <summary>
/// Validator for dealing with UserSkill data entry 
/// </summary>
public class UserSkillValidator : AbstractValidator<UserSkillModificationDto>
{
    public UserSkillValidator()
    {
        RuleSet(RuleSetConstants.PutUserSkillRuleSetName, () =>
        {
            RuleFor(userSkill => userSkill.SkillId).NotNull().NotEmpty().WithMessage(SkillValidationErrors.SkillIdErrorMessage);
            RuleFor(userSkill => userSkill.UserId).NotNull().NotEmpty().WithMessage(UserValidationErrors.UserIdErrorMessage);
            RuleFor(userSkill => userSkill.RatingId).NotNull().NotEmpty().WithMessage(UserSkillValidationErrors.UserSkillRatingIdErrorMessage);
        });
    }
}
