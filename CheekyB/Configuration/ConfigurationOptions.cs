using CheekyServices.Configuration;

namespace CheekyB.Configuration;

public static class ConfigurationOptions
{
    /// <summary>
    /// Configures the Configuration Options.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    public static void ConfigOptions(this IServiceCollection services, IConfiguration configuration)

    {
        var googleConfig = configuration
            .GetSection("GoogleAuthentication")
            .Get<GoogleAuthConfiguration>();

        var jwtConfig = configuration
            .GetSection("JWTConfiguration")
            .Get<JWTConfiguration>();

        services.AddSingleton(googleConfig!);
        services.AddSingleton(jwtConfig!);
    }
}