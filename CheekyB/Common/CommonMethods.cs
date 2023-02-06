using CheekyServices.Exceptions;

namespace CheekyB.Common;

public class CommonMethods
{
    public static IResult ErrorResponseSelector(Exception exception, string message)
    {
        return exception switch
        {
            CheekyExceptions<UserNotFoundException> => Results.NotFound(message),
            CheekyExceptions<UserConflictException> => Results.Conflict(message),
            _ => Results.Problem(detail: message, statusCode: StatusCodes.Status500InternalServerError)
        };
    }
}