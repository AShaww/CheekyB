using System.Runtime.CompilerServices;
using CheekyB.Common;
using CheekyB.Constants.EndpointConstants;
using CheekyModels.Dtos;
using CheekyServices.Exceptions;
using CheekyServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

[assembly: InternalsVisibleTo("CheekyTests")]

namespace CheekyB.Endpoints;

public static class CoreSkillEndpoints
{
    public static RouteGroupBuilder MapCoreSkillEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllCoreSkills)
            .WithName(CoreSkillEndpointConstants.GetAllCoreSkills)
            .Produces<CoreSkillDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/{Id:Guid}", GetCoreSkillById)
            .WithName(CoreSkillEndpointConstants.GetCoreSkillById)
            .Produces<CoreSkillDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("/", InsertCoreSkill)
            .WithName(CoreSkillEndpointConstants.InsertCoreSkill)
            .Produces<CoreSkillDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("/{Id:Guid}", UpdateCoreSkill)
            .WithName(CoreSkillEndpointConstants.UpdateCoreSkill)
            .Produces<CoreSkillDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("/{Id:Guid}", DeleteCoreSkill)
            .WithName(CoreSkillEndpointConstants.DeleteCoreSkill)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);   
        
        return group;
    }

    internal static async Task<IResult> GetAllCoreSkills([FromServices] ICoreSkillService coreSkillService)
    {
        try
        {
            var coreSkills = await coreSkillService.GetAllCoreSkills();

            return Results.Ok(coreSkills);
        }
        
        catch (Exception ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }
    internal static async Task<IResult> GetCoreSkillById([FromServices] ICoreSkillService coreSkillService, Guid id)
    {
        try
        {
            var coreSkill = await coreSkillService.GetCoreSkillById(id);
            
            if (coreSkill == null)
            {
                return Results.NotFound($"CoreSkill with Id {id} not found");
            }

            return Results.Ok(coreSkill);
        }
        
        catch (Exception ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }

    internal static async Task<IResult> InsertCoreSkill([FromServices] ICoreSkillService coreSkillService, [FromBody] CoreSkillDto coreSkillDto)
    {
        try
        {
            var insertedCoreSkill = await coreSkillService.InsertCoreSkill(coreSkillDto);
            return Results.Created($"/api/coreskill/{insertedCoreSkill.CoreSkillId}", insertedCoreSkill);
        }
        
        catch (Exception ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }

    internal static async Task<IResult> UpdateCoreSkill([FromServices] ICoreSkillService coreSkillService, Guid id, [FromBody] CoreSkillDto coreSkillDto)
    {
        try
        {
            if (id != coreSkillDto.CoreSkillId)
            {
                return Results.BadRequest($"Invalid update request for CoreSkill {id}");
            }

            var updatedCoreSkill = await coreSkillService.UpdateCoreSkill(coreSkillDto);
            return Results.Ok(updatedCoreSkill);
        }
        catch (Exception ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }

    internal static async Task<IResult> DeleteCoreSkill([FromServices] ICoreSkillService coreSkillService, Guid id)
    {
        try
        {
            await coreSkillService.DeleteCoreSkill(id);
            return Results.NoContent();
        }
        
        catch (Exception ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }
}
