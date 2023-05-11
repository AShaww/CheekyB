using CheekyData.Implementations;
using CheekyData.Interfaces;
using CheekyServices.Configuration;
using CheekyServices.Implementations;
using CheekyServices.Interfaces;
using CheekyServices.Utilities;

namespace CheekyB.Configuration;

public static class ServiceCollectionExtension
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IToDoRepository, ToDoRepository>();
        services.AddScoped<IToDoService, ToDoService>();
        services.AddScoped<IScrapedNewsRepository, ScrapedNewsRepository>();
        services.AddScoped<IScrapedNewsService, ScrapedNewsService>();
        services.AddSingleton<IUserJwtGenerator, UserJwtGenerator>();
        services.AddScoped<IAuthService, AuthService>();
    }
}
