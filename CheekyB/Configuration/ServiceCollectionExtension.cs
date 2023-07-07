using CheekyData.Implementations;
using CheekyData.Interfaces;
using CheekyServices.Implementations;
using CheekyServices.Interfaces;
using CheekyServices.Utilities;
using NavyPottleData.Implementation;

namespace CheekyB.Configuration;

public static class ServiceCollectionExtension
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<IUserSkillRepository, UserSkillRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserPortfolioRepository, UserPortfolioRepository>();
        services.AddScoped<IUserPortfolioService, UserPortfolioService>();
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<IUserSkillService, UserSkillService>();
        services.AddScoped<IToDoRepository, ToDoRepository>();
        services.AddScoped<IToDoService, ToDoService>();
        services.AddScoped<IScrapedNewsRepository, ScrapedNewsRepository>();
        services.AddScoped<IScrapedNewsService, ScrapedNewsService>();
        services.AddSingleton<IUserJwtGenerator, UserJwtGenerator>();
        services.AddScoped<IAuthService, AuthService>();
    }
}
