using CheekyData.Implementations;
using CheekyData.Interfaces;

namespace CheekyB.Configuration;

public static class ServiceCollectionExtension
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
    }
}