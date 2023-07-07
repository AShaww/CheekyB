using CheekyModels.Dtos;
using CheekyServices.Constants;
using CheekyServices.Constants.ErrorConstants;
using FluentValidation;

namespace CheekyServices.Validators;
/// <summary>
/// Validator for dealing with User data entry 
/// </summary>
public class UserValidator : AbstractValidator<UserDto> 
{
    public UserValidator()
    {
        RuleSet(RuleSetConstants.PutUserRuleSetName, () =>
        {
            RuleFor(user => user.UserId).NotNull().NotEmpty().WithMessage(UserValidationErrors.UserIdErrorMessage);
        });

        RuleFor(user => user.FirstName).NotNull().NotEmpty().WithMessage(UserValidationErrors.UserNameErrorMessage);
        RuleFor(user => user.FirstName).MaximumLength(50).MinimumLength(2).WithMessage(UserValidationErrors.UserNameLengthErrorMessage);
        RuleFor(user => user.Surname).NotNull().NotEmpty().MaximumLength(50).MinimumLength(2).WithMessage(UserValidationErrors.UserNameLengthErrorMessage);
        RuleFor(user => user.Email).EmailAddress().WithMessage(UserValidationErrors.UserEmailErrorMessage);
        RuleFor(user => user.Archived).NotNull().WithMessage(UserValidationErrors.UserArchivedErrorMessage);
    }
}


