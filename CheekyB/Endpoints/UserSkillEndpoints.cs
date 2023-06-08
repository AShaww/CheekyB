
using CheekyB.Common;
using CheekyB.Constants.EndpointConstants;
using CheekyB.Filters;
using CheekyB.Metadata;
using CheekyModels.Dtos;
using CheekyServices.Constants;
using CheekyServices.Exceptions;
using CheekyServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NavyPottleServices.Exceptions;
using Serilog;

namespace CheekyB.Endpoints;

public static class UserSkillEndpoints
{
    public static RouteGroupBuilder MapUserSkillEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllUserSkills)
            .WithName(UserSkillEndpointConstants.GetAllUserSkills)
            .WithTags(UserSkillEndpointConstants.UserSkillWithTag)
            .Produces<IEnumerable<UserSkillDto>>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/{userId:guid}", GetAllUserSkillsByUserId)
            .WithName(UserSkillEndpointConstants.GetAllUserSkillsByUserIdWithName)
            .WithTags(UserSkillEndpointConstants.UserSkillWithTag)
            .Produces<IEnumerable<UserSkillDto>>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);


        group.MapGet("/{userId:guid}/{skillTypeId:int}", GetAllUserSkillsByUserIdAndSkillTypeId)
            .WithName(UserSkillEndpointConstants.GetAllUserSkillsByUserIdAndSkillTypeIdWithName)
            .WithTags(UserSkillEndpointConstants.UserSkillWithTag)
            .Produces<IEnumerable<UserSkillDto>>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("/", InsertUserSkill)
            .WithName(UserSkillEndpointConstants.InsertUserSkillWithName)
            .Accepts<UserSkillModificationDto>("application/json")
            .AddEndpointFilter<ValidationFilter<UserSkillModificationDto>>()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("/", UpdateUserSkill)
            .WithName(UserSkillEndpointConstants.UpdateUserSkillWithName)
            .Accepts<UserSkillModificationDto>("application/json")
            .AddEndpointFilter<ValidationFilter<UserSkillModificationDto>>()
            .WithMetadata(new RuleSetMetadata(RuleSetConstants.PutUserSkillRuleSets))
            .Produces<UserSkillDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("/{userId:Guid}/{skillId:Guid}", DeleteUserSkill)
            .WithName(UserSkillEndpointConstants.DeleteUserSkillWithName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        return group;
    }

    /// <summary>
    /// Calls the UserSkill Service and returns a list of User names and Skills or returns an error 
    /// </summary>
    /// <param name="userSkillService"></param>
    /// <param name="filter"></param>
    /// <returns>List of UserSkills</returns>
    internal static async Task<IResult> GetAllUserSkills(IUserSkillService userSkillService, [AsParameters] UserSkillFilterDto filter)
    {
        var userSkills = await userSkillService.GetAllUserSkills(filter.PageNumber, filter.PageSize,filter.SkillNames);

        return Results.Ok(userSkills);
    }

    /// <summary>
    /// Calls the UserSkill Service and returns a list of all UserSkills by UserId or returns an error 
    /// </summary>
    /// <param name="userSkillService"></param>
    /// <param name="userId"></param>
    /// <returns>List of UserSkills</returns>
    internal static async Task<IResult> GetAllUserSkillsByUserId(IUserSkillService userSkillService, Guid userId)
    {
        try
        {
            var userSkills = await userSkillService.GetAllUserSkillsByUserId(userId);
            return Results.Ok(userSkills);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }

    /// <summary>
    /// Calls the UserSkill Service and returns a list of all UserSkills by UserId and SkillTypeId or returns an error 
    /// </summary>
    /// <param name="userSkillService"></param>
    /// <param name="userId"></param>
    /// <param name="skillTypeId"></param>
    /// <returns>UserSkill</returns>
    internal static async Task<IResult> GetAllUserSkillsByUserIdAndSkillTypeId(IUserSkillService userSkillService, Guid userId, int skillTypeId)
    {
        try
        {

            var userSkills = await userSkillService.GetAllUserSkillsByUserIdAndSkillTypeId(userId, skillTypeId);
            return Results.Ok(userSkills);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }

    /// <summary>
    /// Calls the UserSkill Service and inserts a UserSkill or returns an error 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="userSkillService"></param>
    /// <param name="userSkillToAdd"></param>
    /// <returns>UserSkill</returns>
    internal static async Task<IResult> InsertUserSkill(HttpContext context, [FromServices] IUserSkillService userSkillService,
    [FromBody] UserSkillModificationDto userSkillToAdd)
    {
        try
        {
            var result = await userSkillService.InsertUserSkill(userSkillToAdd);

            var skill = new Uri($"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}/skill");
            var user = new Uri($"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}/user");

            (string skill, string user) combined = (skill.AbsolutePath, user.AbsolutePath);
            return Results.Created(combined.ToString(), result);
        }
        catch (CheekyExceptions<UserSkillNotFoundException> ex)
        {
            Log.Error(ex.Message);
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
        catch (CheekyExceptions<UserSkillConflictException> ex)
        {
            Log.Error(ex.Message);
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }

    }

    /// <summary>
    /// Calls the UserSkill Service and updates a UserSkill based on a UserSkillId or returns an error 
    /// </summary>
    /// <param name="userSkillService"></param>
    /// <param name="userSkillToUpdate"></param>
    /// <returns>Updated UserSkill</returns>
    internal static async Task<IResult> UpdateUserSkill([FromServices] IUserSkillService userSkillService,
    [FromBody] UserSkillModificationDto userSkillToUpdate)
    {
        try
        {
            var result = await userSkillService.UpdateUserSkill(userSkillToUpdate);
            return Results.Ok(result);
        }
        catch (CheekyExceptions<UserSkillNotFoundException> ex)
        {
            Log.Error(ex.Message);
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
        catch (CheekyExceptions<UserSkillConflictException> ex)
        {
            Log.Error(ex.Message);
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }

    /// <summary>
    /// Calls the UserSkill Service and deletes a UserSkill based on a UserSkillId or returns an error 
    /// </summary>
    /// <param name="userSkillService"></param>
    /// <param name="userId"></param>
    /// <param name="skillId"></param>
    /// <returns>Deleted UserSkill</returns>
    internal static async Task<IResult> DeleteUserSkill([FromServices] IUserSkillService userSkillService, Guid userId, Guid skillId)
    {
        try
        {
            var result = await userSkillService.DeleteUserSkill(userId, skillId);
            return Results.Ok(result);
        }
        catch (CheekyExceptions<UserSkillNotFoundException> ex)
        {
            Log.Error(ex.Message);
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }
}
