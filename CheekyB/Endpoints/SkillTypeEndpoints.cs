using System.Runtime.CompilerServices;
using CheekyB.Common;
using CheekyB.Constants.EndpointConstants;
using CheekyServices.Interfaces;
using CheekyModels.Dtos;
using CheekyServices.Exceptions;
using CheekyServices.Exceptions.SkillTypesExceptions;
using Microsoft.AspNetCore.Mvc;

[assembly: InternalsVisibleTo("CheekyTests")]

namespace CheekyB.Endpoints
{
    public static class SkillTypeEndpoints
    {
        public static RouteGroupBuilder MapSkillTypeEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAllSkillTypes)
                .WithName(SkillTypeEndpointConstants.GetAllSkillTypes)
                .Produces<SkillTypeDto>()
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            group.MapGet("/{skillTypeId:int}", GetSkillTypeById)
                .WithName(SkillTypeEndpointConstants.GetCoreSkillTypeById)
                .Produces<SkillTypeDto>()
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            group.MapPost("/", InsertSkillType)
                .WithName(SkillTypeEndpointConstants.InsertSkillType)
                .Produces<SkillTypeDto>()
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            group.MapPut("/", UpdateSkillType)
                .WithName(SkillTypeEndpointConstants.UpdateSkillType)
                .Produces<SkillTypeDto>()
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            group.MapDelete("/{skillTypeId:int}", DeleteSkillType)
                .WithName(SkillTypeEndpointConstants.DeleteSkillType)
                .Produces<SkillTypeDto>()
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            return group;
        }

        internal static async Task<IResult> GetAllSkillTypes([FromServices] ISkillTypeService skillTypeService)
        {
            try
            {
                var skillTypes = await skillTypeService.GetAllSkillTypes();
                return Results.Ok(skillTypes);
            }
            catch (Exception ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
        }

        internal static async Task<IResult> GetSkillTypeById([FromServices] ISkillTypeService skillTypeService, int skillTypeId)
        {
            try
            {
                var skillType = await skillTypeService.GetSkillTypeById(skillTypeId);
                
                if (skillType == null)
                {
                    return Results.NotFound($"SkillType with Id {skillTypeId} not found");
                }

                return Results.Ok(skillType);
            }
            catch (Exception ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
        }

        internal static async Task<IResult> InsertSkillType([FromServices] ISkillTypeService skillTypeService, [FromBody] SkillTypeDto skillTypeDto)
        {
            try
            {
                var insertedSkillType = await skillTypeService.InsertSkillType(skillTypeDto);
                return Results.Created($"/api/skilltype/{insertedSkillType.SkillTypeId}", insertedSkillType);
            }
            catch (Exception ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
        }

        internal static async Task<IResult> UpdateSkillType([FromServices] ISkillTypeService skillTypeService, [FromBody] SkillTypeDto skillTypeDto)
        {
            try
            {
                var updatedSkillType = await skillTypeService.UpdateSkillType(skillTypeDto);
                return Results.Ok(updatedSkillType);
            }
            catch (CheekyExceptions<SkillTypeNotFoundException> ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
            catch (Exception ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
        }

        internal static async Task<IResult> DeleteSkillType([FromServices] ISkillTypeService skillTypeService, int skillTypeId)
        {
            try
            {
                var deletedSkillType = await skillTypeService.DeleteSkillType(skillTypeId);
                return Results.Ok(deletedSkillType);
            }
            catch (CheekyExceptions<SkillTypeNotFoundException> ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
            catch (Exception ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
        }
    }
}
