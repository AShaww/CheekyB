using CheekyB.Constants.EndpointConstants;
using CheekyModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CheekyB.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllUsers)
            .WithName(UserEndpointConstants.GetAllUsersWithName)
            .Produces<IEnumerable<UserDto>>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/{userId:Guid}", GetUserById)
            .WithName(UserEndpointConstants.GetUserByUserIdWithName)
            .Produces<UserDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("/", InsertUser)
            .WithName(UserEndpointConstants.InsertUserWithName)
            .Accepts<UserDto>("application/json")
            .AddEndpointFilter<ValidationFilter<UserDto>>()
            .Produces<UserDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("/", UpdateUser)
            .WithName(UserEndpointConstants.UpdateUserWithName)
            .Accepts<UserDto>("application/json")
            .AddEndpointFilter<ValidationFilter<UserDto>>()
            .WithMetadata(new RuleSetMetadata(RuleSetConstants.PutUserRuleSets))
            .Produces<UserDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("/{userId:Guid}", DeleteUser)
            .WithName(UserEndpointConstants.DeleteUserWithName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);


        return group;
    }

    // Uncomment when UI part is ready and if need to add this attribute
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    /// <summary>
    /// Calls the User Service and returns a list of all Users or returns an error 
    /// </summary>
    /// <param name="userService"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    internal static async Task<IResult> GetAllUsers([FromServices] IUserService userService, int pageNumber, int pageSize)
    {
        try
        {
            var users = await userService.GetAllUsers(pageNumber, pageSize);

            return Results.Ok(users);
        }
        catch (Exception ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }

    // Uncomment when UI part is ready and if need to add this attribute
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    /// <summary>
    /// Calls the User Service and returns a User based on a UserId or returns an error 
    /// </summary>
    /// <param name="userService"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    internal static async Task<IResult> GetUserById([FromServices] IUserService userService, Guid userId)
    {
        try
        {
            var user = await userService.GetUserById(userId);

            return Results.Ok(user);
        }
        catch (CheekyExceptions<UserNotFoundException> ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
        catch (Exception ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }

    // Uncomment when UI part is ready and if need to add this attribute
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    /// <summary>
    /// Calls the User Service and inserts a User or returns an error 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="userService"></param>
    /// <param name="userToAdd"></param>
    /// <returns></returns>
    internal static async Task<IResult> InsertUser(HttpContext context, [FromServices] IUserService userService,
        [FromBody] UserDto userToAdd)
    {
        try
        {
            var result = await userService.InsertUser(userToAdd);
            return Results.Created(
                new Uri($"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}/{result.UserId}"),
                result);
        }
        catch (CheekyExceptions<UserConflictException> ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
        catch (Exception ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }

    /// <summary>
    /// Calls the User Service and Uupdates a User based on a UserId or returns an error 
    /// </summary>
    /// <param name="userService"></param>
    /// <param name="userToUpdate"></param>
    /// <returns></returns>
    internal static async Task<IResult> UpdateUser([FromServices] IUserService userService,
        [FromBody] UserDto userToUpdate)
    {
        try
        {
            var result = await userService.UpdateUser(userToUpdate);

            return Results.Ok(result);
        }
        catch (CheekyExceptions<UserNotFoundException> ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
        catch (CheekyExceptions<UserConflictException> ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
        catch (Exception ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }

    /// <summary>
    /// Calls the User Service and deletes a User based on a UserId or returns an error 
    /// </summary>
    /// <param name="userService"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    internal static async Task<IResult> DeleteUser([FromServices] IUserService userService, Guid userId)
    {
        try
        {
            var result = await userService.SoftDeleteUser(userId);
            return Results.Ok(result);
        }
        catch (CheekyExceptions<UserNotFoundException> ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
        catch (Exception ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }
}