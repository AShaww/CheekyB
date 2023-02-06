using CheekyData.Implementations;
using CheekyData.Interfaces;
using CheekyServices.Implementations;
using CheekyServices.Interfaces;

namespace CheekyB.Configuration;

public static class ServiceCollectionExtension
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
    }
}