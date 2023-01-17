using ConfigureCount;
using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigureCount;

public static class Startup
{
    private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();

    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        ConfigureLoggingAndConfigurations(services);
        ConfigureApplicationServices(services);

        IServiceProvider provider = services.BuildServiceProvider();

        return provider;
    }


    private static void ConfigureLoggingAndConfigurations(IServiceCollection services)
    {
        // Add configuration service
        services.AddSingleton<IConfiguration>(Configuration!);
        services.AddWithValidation<DataArchiveOptions, DataArchiveOptionsValidator>("DataArchiveOptions");
    }

    private static void ConfigureApplicationServices(IServiceCollection services)
    {
        services.AddScoped<ITestService, TestService>();
    }
}
