using System.Runtime.CompilerServices;
using AutoMapper;
using CheekyB.Common;
using CheekyB.Constants.EndpointConstants;
using CheekyB.Filters;
using CheekyB.Metadata;
using CheekyModels.Dtos;
using CheekyServices.Constants;
using CheekyServices.Exceptions;
using CheekyServices.Exceptions.UserPortfolioExceptions;
using CheekyServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

using Serilog;

[assembly: InternalsVisibleTo("CheekyTests")]

namespace CheekyB.Endpoints
{
    public static class UserPortfolioEndpoints
    {
        public static RouteGroupBuilder MapUserPortfolioEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/{userId:Guid}", GetPortfolioByUserId)
                .WithName(UserPortfolioEndpointConstants.GetPortfolioByUserIdWithName)
                .Produces<UserPortfolioDto>()
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            group.MapPost("/", InsertUserPortfolio)
                .WithName(UserPortfolioEndpointConstants.InsertUserPortfolioWithName)
                .Accepts<UserPortfolioDto>("application/json")
                .AddEndpointFilter<ValidationFilter<UserPortfolioDto>>()
                .Produces(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            group.MapPut("/", UpdateUserPortfolio)
                .WithName(UserPortfolioEndpointConstants.UpdateUserPortfolioWithName)
                .Accepts<UserPortfolioDto>("application/json")
                .AddEndpointFilter<ValidationFilter<UserPortfolioDto>>()
                .WithMetadata(new RuleSetMetadata(RuleSetConstants.PutUserPortfolioRuleSets))
                .Produces<UserPortfolioDto>()
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            return group;
        }
        /// <summary>
        /// Calls the UserPortfolio Service and returns a list of all UserPortfolios by UserId or returns an error 
        /// </summary>
        /// <param name="userPortfolioService"></param>
        /// <param name="userId"></param>
        /// <returns>UserPortfolio</returns>
        internal static async Task<IResult> GetPortfolioByUserId([FromServices] IUserPortfolioService userPortfolioService, Guid userId)
        {
            try
            {
                var portfolio = await userPortfolioService.GetUserPortfolioByUserId(userId);

                return Results.Ok(portfolio);
            }
            catch (CheekyExceptions<UserPortfolioNotFoundException> ex)
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
        /// Calls the UserPortfolio Service and inserts a UserPortfolios or returns an error 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="context"></param>
        /// <param name="userPortfolioService"></param>
        /// <param name="portfolioToAdd"></param>
        /// <returns>UserPortfolio</returns>
        internal static async Task<IResult> InsertUserPortfolio(IMapper mapper, HttpContext context, [FromServices] IUserPortfolioService userPortfolioService, UserPortfolioDto portfolioToAdd)
        {
            try
            {
                var result = await userPortfolioService.InsertUserPortfolio(portfolioToAdd);

                return Results.Created(new Uri($"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}/{result.UserId}"),
                    result);
            }
            catch (CheekyExceptions<UserPortfolioConflictException> ex)
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
        /// Calls the UserPortfolio Service and updates a UserPortfolio using an UserPortfolioId or returns an error 
        /// </summary>
        /// <param name="userPortfolioService"></param>
        /// <param name="userService"></param>
        /// <param name="portfolioToUpdate"></param>
        /// <returns>UserPortfolio</returns>
        internal static async Task<IResult> UpdateUserPortfolio([FromServices] IUserPortfolioService userPortfolioService, [FromServices] IUserService userService, UserPortfolioDto portfolioToUpdate)
        {
            try
            {
               var results = await userPortfolioService.UpdateUserPortfolio(portfolioToUpdate);
               var updateUser = await userService.UpdateUser(new UserDto
               {
                   UserId = portfolioToUpdate.UserId,
                   FirstName = portfolioToUpdate.FirstName,
                   Surname = portfolioToUpdate.Surname,
                   Email = portfolioToUpdate.Email
               });
                return Results.Ok(results);
            }
            catch (CheekyExceptions<UserPortfolioBadRequestException> ex)
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
}
