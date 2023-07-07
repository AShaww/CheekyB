using CheekyServices.Exceptions;
using CheekyServices.Exceptions.RatingExceptions;
using CheekyServices.Exceptions.SkillExceptions;
using CheekyServices.Exceptions.ToDoExceptions;
using CheekyServices.Exceptions.UserPortfolioExceptions;
using NavyPottleServices.Exceptions;

namespace CheekyB.Common;

public static class CommonMethods
{
    public static IResult ErrorResponseSelector(Exception exception, string message)
    {
        return exception switch
        {
            CheekyExceptions<UserNotFoundException> or CheekyExceptions<SkillNotFoundException> or CheekyExceptions<UserPortfolioNotFoundException> or CheekyExceptions<UserSkillNotFoundException> or CheekyExceptions<RatingNotFoundException> or CheekyExceptions<ToDoNotFoundException> => Results.NotFound(message),
            CheekyExceptions<UserConflictException> or CheekyExceptions<SkillConflictException> or CheekyExceptions<UserPortfolioConflictException> or CheekyExceptions<UserSkillConflictException> or CheekyExceptions<RatingConflictException> or CheekyExceptions<ToDoConflictException> => Results.Conflict(message),
            CheekyExceptions<UserPortfolioBadRequestException> => Results.BadRequest(message),
            _ => Results.Problem(detail: message, statusCode: StatusCodes.Status500InternalServerError)
        };
    }
}
