using CheekyB.Constants.EndpointConstants;
using CheekyServices.Interfaces;

namespace CheekyB.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapGet("/api/login", async (IAuthService authService, HttpContext context) =>
            {
                var token = context.Request.Headers["google-token"];
                var googleSubjectId = await authService.Login(token.ToString());

                return Results.Ok(googleSubjectId);
            })
            .AllowAnonymous()
            .WithName(AuthEndpointConstants.LoginWithName)
            .WithTags(AuthEndpointConstants.AuthWithTag)
            .Produces<string>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}