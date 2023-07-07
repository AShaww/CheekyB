using CheekyModels.Dtos;
using CheekyServices.Constants.ErrorConstants;
using FluentValidation;

namespace CheekyServices.Validators;
/// <summary>
/// Validator for dealing with UserPortfolio data entry 
/// </summary>
public class UserPortfolioValidator : AbstractValidator<UserPortfolioDto>
{
    public UserPortfolioValidator()
    {
        RuleFor(user => user.UserId).NotNull().NotEmpty().WithMessage(UserValidationErrors.UserIdErrorMessage);
    }
}
