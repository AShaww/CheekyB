using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using CheekyB.Configuration;
using CheekyB.Endpoints;
using CheekyB.Extensions;
using CheekyData;
using CheekyServices.Validators;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog
builder.AddSerilog();

// Add configurations
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.Sources.Clear();
    var env = hostingContext.HostingEnvironment;
    
    config.SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) //load base settings
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true) //load environment settings
        .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true) //load local settings
        .AddEnvironmentVariables();

    if (args != null)
    {
        config.AddCommandLine(args);
    }
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["GoogleAuthentication:ClientId"];
        options.ClientSecret = builder.Configuration["GoogleAuthentication:ClientSecret"];
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateActor = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWTConfiguration:Issuer"],
            ValidAudience = builder.Configuration["JWTConfiguration:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfiguration:Key"]))
        };
    });

builder.Services.AddAuthorization(opt =>
{
    opt.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Description = "Bearer Authentication with JWT Token",
            Type = SecuritySchemeType.Http
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme }
            },
            new List<string>()
        }
    });
});

builder.Services.Configure<JsonOptions>(opt =>
{
    opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<CheekyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CheekyContext")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDomainServices();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddValidatorsFromAssemblyContaining<ToDoValidator>();
builder.Services.ConfigOptions(builder.Configuration);
builder.Services.AddCors();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        await response.WriteAsJsonAsync(new { error = "Unauthorized" });
    }
});

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
        .Get<IExceptionHandlerPathFeature>()
        ?.Error;
    if (exception is SecurityTokenExpiredException)
    {
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        await context.Response.WriteAsJsonAsync(new { error = "Unauthorized" });
    }
}));

app.UseHttpsRedirection();

app.UseRouting();

app.MapGroup("api/User")
    .MapUserEndpoints()
    .WithTags("User");

app.MapGroup("api/ToDo")
    .MapToDoEndpoints()
    .WithTags("ToDo");

app.MapGroup("api/ScrapedNews")
    .MapScrapedNewsEndpoints()
    .WithTags("ScrapedNews");

app.MapGroup("api/CoreSkill")
    .MapCoreSkillEndpoints()
    .WithTags("Core Skill");

app.MapGroup("api/TrainedSkill")
    .MapTrainedSkillEndpoints()
    .WithTags("Trained Skill");

app.MapGroup("api/SkillType")
    .MapSkillTypeEndpoints()
    .WithTags("Skill Type");

app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoints();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync($"Welcome to the Cheekiest Api - Environment: {app.Environment.EnvironmentName}");
});

// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapGet("/", async context =>
//     {
//         await context.Response.WriteAsync($"Welcome to the Cheekiest Api - Environment: {app.Environment.EnvironmentName}");
//     });
// });

app.Run();

// Need for Tests
public partial class Program { }