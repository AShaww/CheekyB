using System.Runtime.CompilerServices;
using AutoMapper;
using CheekyB.Common;
using CheekyB.Constants.EndpointConstants;
using CheekyB.Filters;
using CheekyB.Metadata;
using CheekyData.Models;
using CheekyModels.Dtos;
using CheekyServices.Constants;
using CheekyServices.Exceptions;
using CheekyServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

[assembly: InternalsVisibleTo("CheekyTests")]

namespace CheekyB.Endpoints;

public static class ToDoEndpoints
{
    public static RouteGroupBuilder MapToDoEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/{userId:Guid}", GetToDoByUserId)
            .WithName(ToDoEndpointConstants.GetToDoByUserIdWithName)
            .Produces<ToDoDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
 
        group.MapPost("/", InsertToDo)
            .WithName(ToDoEndpointConstants.InsertToDoWithName)
            .Accepts<ToDoDto>("application/json")
            .AddEndpointFilter<ValidationFilter<ToDoDto>>()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("/", UpdateToDo)
            .WithName(ToDoEndpointConstants.UpdateToDoWithName)
            .Accepts<ToDoDto>("application/json")
            .AddEndpointFilter<ValidationFilter<ToDoDto>>()
            .WithMetadata(new RuleSetMetadata(RuleSetConstants.PutToDoRuleSets))
            .Produces<ToDoDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        return group;
    }
     internal static async Task<IResult> GetToDoByUserId([FromServices] IToDoService toDoService, Guid userId)
        {
            try
            {
                var toDo = await toDoService.GetTodoByUserId(userId);

                return Results.Ok(toDo);
            }
            catch (CheekyExceptions<ToDoNotFoundException> ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
            catch (Exception ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
        }

        internal static async Task<IResult> InsertToDo(IMapper mapper, HttpContext context, [FromServices] IToDoService toDoService, ToDoDto toDoToAdd)
        {
            try
            {
                var result = await toDoService.InsertTodo(toDoToAdd);

                return Results.Created(new Uri($"{context.Request.Scheme}://" +
                                               $"{context.Request.Host}{context.Request.Path}/{result.ToDoId}"), result);
            }
            catch (CheekyExceptions<ToDoConflictException> ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
            catch (Exception ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
        }

        internal static async Task<IResult> UpdateToDo([FromServices] IToDoService toDoService, [FromServices] IUserService userService, ToDoDto toDoToUpdate)
        {
            try
            {
               var results = await toDoService.UpdateTodo(toDoToUpdate);
             
                return Results.Ok(results);
            }
            catch (CheekyExceptions<ToDoBadRequestException> ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
            catch (Exception ex)
            {
                return CommonMethods.ErrorResponseSelector(ex, ex.Message);
            }
        }
}