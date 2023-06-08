using System.Runtime.CompilerServices;
using CheekyB.Common;
using CheekyB.Constants.EndpointConstants;
using CheekyServices.Interfaces;
using CheekyModels.Dtos;
using CheekyServices.Exceptions;
using CheekyServices.Exceptions.TrainedSkillExceptions;
using Microsoft.AspNetCore.Mvc;

[assembly: InternalsVisibleTo("CheekyTests")]

namespace CheekyB.Endpoints
{
    public static class TrainedSkillEndpoints
    {
        public static RouteGroupBuilder MapTrainedSkillEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAllTrainedSkills)
                .WithName(TrainedSkillEndpointConstants.GetAllTrainedSkill)
                .Produces<TrainedSkillDto>()
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            group.MapGet("/{trainedSkillId:Guid}", GetTrainedSkillById)
                .WithName(TrainedSkillEndpointConstants.GetTrainedSkillById)
                .Produces<TrainedSkillDto>()
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            group.MapPost("/", InsertTrainedSkill)
                .WithName(TrainedSkillEndpointConstants.InsertTrainedSkill)
                .Produces<TrainedSkillDto>()
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            group.MapPut("/", UpdateTrainedSkill)
                .WithName(TrainedSkillEndpointConstants.UpdateTrainedSkill)
                .Produces<TrainedSkillDto>()
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            group.MapDelete("/{trainedSkillId:Guid}", DeleteTrainedSkill)
                .WithName(TrainedSkillEndpointConstants.DeleteTrainedSkill)
                .Produces<TrainedSkillDto>()
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            return group;
        }

        internal static async Task<IResult> GetAllTrainedSkills([FromServices] ITrainedSkillService trainedSkillService)
        {
            try
            {
                var trainedSkills = await trainedSkillService.GetAllTrainedSkills();
                return Results.Ok(trainedSkills);
            }
            catch (Exception ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
        }

        internal static async Task<IResult> GetTrainedSkillById([FromServices] ITrainedSkillService trainedSkillService, Guid trainedSkillId)
        {
            try
            {
                var trainedSkill = await trainedSkillService.GetTrainedSkillById(trainedSkillId);
                
                if (trainedSkill == null)
                {
                    return Results.NotFound($"TrainedSkill with Id {trainedSkillId} not found");
                }

                return Results.Ok(trainedSkill);
            }
            catch (Exception ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
        }

        internal static async Task<IResult> InsertTrainedSkill([FromServices] ITrainedSkillService trainedSkillService, [FromBody] TrainedSkillDto trainedSkillDto)
        {
            try
            {
                var insertedTrainedSkill = await trainedSkillService.InsertTrainedSkill(trainedSkillDto);
                return Results.Created($"/api/trainedskills/{insertedTrainedSkill.TrainedSkillId}", insertedTrainedSkill);
            }
            catch (Exception ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
        }

        internal static async Task<IResult> UpdateTrainedSkill([FromServices] ITrainedSkillService trainedSkillService, [FromBody] TrainedSkillDto trainedSkillDto)
        {
            try
            {
                var updatedTrainedSkill = await trainedSkillService.UpdateTrainedSkill(trainedSkillDto);
                return Results.Ok(updatedTrainedSkill);
            }
            catch (CheekyExceptions<TrainedSkillNotFoundException> ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
            catch (Exception ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
        }

        internal static async Task<IResult> DeleteTrainedSkill([FromServices] ITrainedSkillService trainedSkillService, Guid trainedSkillId)
        {
            try
            {
                var deletedTrainedSkill = await trainedSkillService.DeleteTrainedSkill(trainedSkillId);
                
                return Results.Ok(deletedTrainedSkill);
            }
            catch (CheekyExceptions<TrainedSkillNotFoundException> ex)
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
