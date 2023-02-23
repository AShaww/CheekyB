using CheekyServices.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CheekyB.Common;

public class CommonMethods
{
    public static IResult ErrorResponseSelector(Exception exception, string message)
    {
        return exception switch
        {
            CheekyExceptions<UserNotFoundException> or CheekyExceptions<ToDoNotFoundException> =>
                Results.NotFound(message),
            CheekyExceptions<UserConflictException> or CheekyExceptions<ToDoConflictException> =>
                Results.Conflict(message),
            CheekyExceptions<ToDoBadRequestException> => Results.BadRequest(message),
            _ => Results.Problem(detail: message, statusCode: StatusCodes.Status500InternalServerError)
        };
    }
}