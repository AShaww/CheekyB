using System.Runtime.CompilerServices;
using CheekyB.Common;
using CheekyB.Constants.EndpointConstants;
using CheekyModels.Dtos;
using CheekyServices.Exceptions;
using CheekyServices.Exceptions.ScrapedNewsExceptions;
using CheekyServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

[assembly: InternalsVisibleTo("CheekyTests")]

namespace CheekyB.Endpoints;

public static class ScrapedNewsEndpoints
{
    public static RouteGroupBuilder MapScrapedNewsEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllScrapedNews)
            .WithName(ScrapedNewsConstants.GetAllScrapedNews)
            .Produces<ScrapedNewsDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("/{NewsId:Guid}", DeleteScrapedNews)
            .WithName(ScrapedNewsConstants.DeleteScrapedNews)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);   
        
        return group;
    }

    internal static async Task<IResult> GetAllScrapedNews([FromServices] IScrapedNewsService scrapedNewsService)
    {
        try
        {
            var scrapedNews = await scrapedNewsService.GetAllScrapedNews();

            return Results.Ok(scrapedNews);
        }
        catch (Exception ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }

    internal static async Task<IResult> DeleteScrapedNews([FromServices] IScrapedNewsService scrapedNewsService,
        Guid newsId)
    {
        try
        {
            var result = await scrapedNewsService.DeleteScrapedNews(newsId);

            return Results.Ok(result);
        }
        catch (CheekyExceptions<ScrapedNewsNotFoundException> ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
        catch (Exception ex)
        {
            return CommonMethods.ErrorResponseSelector(ex, ex.Message);
        }
    }
}