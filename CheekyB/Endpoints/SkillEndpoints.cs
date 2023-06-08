using System.Runtime.CompilerServices;
using CheekyB.Common;
using CheekyB.Constants.EndpointConstants;
using CheekyB.Filters;
using CheekyB.Metadata;
using CheekyModels.Dtos;
using CheekyServices.Constants;
using CheekyServices.Exceptions;
using CheekyServices.Exceptions.SkillExceptions;
using CheekyServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

using Serilog;

[assembly: InternalsVisibleTo("CheekyTests")]

namespace CheekyB.Endpoints;

public static class SkillEndpoints
{
    public static RouteGroupBuilder MapSkillEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllSkills)
            .WithName(SkillEndpointConstants.GetAllSkillsWithName)
            .Produces<IEnumerable<SkillDto>>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/{skillId:Guid}", GetSkillById)
            .WithName(SkillEndpointConstants.GetSkillBySkillIdWithName)
            .Produces<SkillDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/skilltype/{skillTypeId:int}", GetAllSkillsBySkillTypeId)
            .WithName(SkillEndpointConstants.GetSkillBySkillTypeIdWithName)
            .Produces<IEnumerable<SkillDto>>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("/", InsertSkill)
            .WithName(SkillEndpointConstants.InsertSkillWithName)
            .Accepts<SkillModificationDto>("application/json")
            .AddEndpointFilter<ValidationFilter<SkillModificationDto>>()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("/", UpdateSkill)
            .WithName(SkillEndpointConstants.UpdateSkillWithName)
            .Accepts<SkillModificationDto>("application/json")
            .AddEndpointFilter<ValidationFilter<SkillModificationDto>>()
            .WithMetadata(new RuleSetMetadata(RuleSetConstants.PutToDoRuleSets))
            .Produces<SkillDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("/{skillId:Guid}", DeleteSkill)
            .WithName(SkillEndpointConstants.DeleteSkillWithName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        return group;
    }

    /// <summary>
    /// Calls the Skill Service and returns a list of all skills or returns an error 
    /// </summary>
    /// <param name="skillService"></param>
    /// <returns>List of Skills</returns>
    internal static async Task<IResult> GetAllSkills([FromServices] ISkillService skillService)
    {
        try
        {
            var skills = await skillService.GetAllSkills();

            return Results.Ok(skills);
        }
        catch (Exception ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }

    }

    /// <summary>
    /// Calls the Skill Service and returns a Skills by SkillId or returns an error 
    /// </summary>
    /// <param name="skillService"></param>
    /// <param name="skillId"></param>
    /// <returns>Skill</returns>
    internal static async Task<IResult> GetSkillById(ISkillService skillService, Guid skillId)
    {
        try
        {
            var skill = await skillService.GetSkillById(skillId);

            return Results.Ok(skill);
        }
        catch (CheekyExceptions<SkillNotFoundException> ex)
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
    /// Calls the Skill Service and returns a list of all skills by SkillTypeId or returns an error 
    /// </summary>
    /// <param name="skillService"></param>
    /// <param name="skillTypeId"></param>
    /// <returns>Skill</returns>
    internal static async Task<IResult> GetAllSkillsBySkillTypeId(ISkillService skillService, int skillTypeId)
    {
        try
        {
            var skill = await skillService.GetAllSkillsBySkillTypeId(skillTypeId);
            return Results.Ok(skill);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }

    /// <summary>
    /// Calls the Skill Service and inserts and skill or returns an error 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="skillService"></param>
    /// <param name="skillToAdd"></param>
    /// <returns>Skill</returns>
    internal static async Task<IResult> InsertSkill(HttpContext context, [FromServices] ISkillService skillService,
     [FromBody] SkillModificationDto skillToAdd)
    {
        try
        {
            var result = await skillService.InsertSkill(skillToAdd);
            return Results.Created(new Uri($"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}/{result.SkillId}"),
                    result);
        }
        catch (CheekyExceptions<SkillConflictException> ex)
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
    /// Calls the Skill Service and updates a skill based on an Id or returns an error 
    /// </summary>
    /// <param name="skillService"></param>
    /// <param name="skillToUpdate"></param>
    /// <returns>Updated Skill</returns>
    internal static async Task<IResult> UpdateSkill([FromServices] ISkillService skillService,
        [FromBody] SkillModificationDto skillToUpdate)
    {
        try
        {
            var result = await skillService.UpdateSkill(skillToUpdate);
            return Results.Ok(result);
        }
        catch (CheekyExceptions<SkillNotFoundException> ex)
        {
            Log.Error(ex.Message);
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
        catch (CheekyExceptions<SkillConflictException> ex)
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
    /// Calls the Skill Service and deletes a skill based on an Id or returns an error 
    /// </summary>
    /// <param name="skillService"></param>
    /// <param name="skillId"></param>
    /// <returns>Deleted Skill</returns>
    internal static async Task<IResult> DeleteSkill([FromServices] ISkillService skillService, Guid skillId)
    {
        try
        {
            var result = await skillService.DeleteSkill(skillId);
            return Results.Ok(result);
        }
        catch (CheekyExceptions<SkillNotFoundException> ex)
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

