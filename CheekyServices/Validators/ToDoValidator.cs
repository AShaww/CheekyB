using CheekyModels.Dtos;
using CheekyServices.Constants;
using CheekyServices.Constants.ErrorConstants;
using FluentValidation;

namespace CheekyServices.Validators;

public class ToDoValidator : AbstractValidator<ToDoDto>
{
    public ToDoValidator()
    {
        RuleSet(RuleSetConstants.PutToDoRuleSetName,
            () =>
            {
                //RuleFor(toDo => toDo.ToDoId).NotNull().NotEmpty().WithMessage(ToDoValidationErrors.ToDoIdErrorMessage);
            });

        /*
        RuleFor(toDo => toDo.ToDoTitle).NotNull().NotEmpty().MaximumLength(50).MinimumLength(2)
            .WithMessage(ToDoValidationErrors.ToDoTitleErrorMessage);
        RuleFor(toDo => toDo.ToDoMessage).MaximumLength(250).MinimumLength(2)
            .WithMessage(ToDoValidationErrors.ToDoMessageLengthErrorMessage);*/
    }
}