using System.Runtime.CompilerServices;
using AutoMapper;
using CheekyB.Common;
using CheekyB.Constants.EndpointConstants;
using CheekyB.Filters;
using CheekyB.Metadata;
using CheekyModels.Dtos;
using CheekyServices.Constants;
using CheekyServices.Exceptions;
using CheekyServices.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[assembly: InternalsVisibleTo("CheekyTests")]

namespace CheekyB.Endpoints;

public static class ToDoEndpoints
{
    public static RouteGroupBuilder MapToDoEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/{toDoId:Guid}", GetToDoByToDoId)
            .WithName(ToDoEndpointConstants.GetToDoByToDoId)
            .Produces<ToDoDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/", GetAllToDos)
            .WithName(ToDoEndpointConstants.GetAllToDos)
            .Produces<IEnumerable<ToDoDto>>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);


        group.MapPost("/", InsertToDo)
            .WithName(ToDoEndpointConstants.InsertToDo)
            .Accepts<ToDoDto>("application/json")
            .AddEndpointFilter<ValidationFilter<ToDoDto>>()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("/", UpdateToDo)
            .WithName(ToDoEndpointConstants.UpdateToDo)
            .Accepts<ToDoDto>("application/json")
            .AddEndpointFilter<ValidationFilter<ToDoDto>>()
            .WithMetadata(new RuleSetMetadata(RuleSetConstants.PutToDoRuleSets))
            .Produces<ToDoDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        group.MapDelete("/{toDoId:Guid}", DeleteToDo)
            .WithName(ToDoEndpointConstants.DeleteToDo)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        return group;
    }

    internal static async Task<IResult> GetAllToDos([FromServices] IToDoService toDoService)
    {
        try
        {
            var toDos = await toDoService.GetAllToDos();

            return Results.Ok(toDos);
        }
        catch (Exception ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }

    internal static async Task<IResult> GetToDoByToDoId([FromServices] IToDoService toDoService, Guid toDoId)
    {
        try
        {
            var toDo = await toDoService.GetTodoByUserId(toDoId);

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

    internal static async Task<IResult> InsertToDo([FromServices] IToDoService toDoService, IMapper mapper, HttpContext context, ToDoDto toDoToAdd)
    {
        try
        {
            var result = await toDoService.InsertTodo(toDoToAdd);

            return Results.Created(new Uri($"{context.Request.Scheme}://" + $"{context.Request.Host}{context.Request.Path}/{result.ToDoId}"), result);
                                          
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

    internal static async Task<IResult> UpdateToDo([FromServices] IToDoService toDoService,
        [FromServices] IUserService userService, ToDoDto toDoToUpdate)
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

    internal static async Task<IResult> DeleteToDo([FromServices] IToDoService toDoService, Guid toDoId)
    {
        try
        {
            var result = await toDoService.DeleteTodo(toDoId);
            return Results.Ok(result);
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
}