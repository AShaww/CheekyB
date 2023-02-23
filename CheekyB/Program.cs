using System.Text.Json.Serialization;
using CheekyB.Configuration;
using CheekyB.Endpoints;
using CheekyData;
using CheekyServices.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddAuthorization();

builder.Services.Configure<JsonOptions>(opt =>
{
    opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<CheekyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CheekyContext")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDomainServices();
builder.Services.AddValidatorsFromAssemblyContaining<ToDoValidator>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors();

var app = builder.Build();

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

app.UseHttpsRedirection();

app.UseRouting();

app.MapGroup("api/User")
    .MapUserEndpoints()
    .WithTags("User");
app.MapGroup("api/ToDo")
    .MapToDoEndpoints()
    .WithTags("ToDo");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync($"Welcome to the Cheekiest Api - Environment: {app.Environment.EnvironmentName}");
    });
});

app.Run();

// Need for Tests
public partial class Program { }