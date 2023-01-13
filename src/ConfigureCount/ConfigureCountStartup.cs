using Amazon.Extensions.NETCore.Setup;
using Core;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConfigureCount;

public class ConfigureCountStartup
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection(); // ServiceCollection require Microsoft.Extensions.DependencyInjection NuGet package

        ConfigureLoggingAndOptions(services);
        ConfigureApplicationServices(services);

        IServiceProvider provider = services.BuildServiceProvider();

        return provider;
    }


    private static void ConfigureLoggingAndOptions(IServiceCollection services)
    {
        // Add configuration service
        services.AddSingleton<IConfiguration>(LambdaConfiguration.Instance);
        
        services.AddScoped<IValidator<DataArchiveOptions>, DataArchiveOptionsValidator>();
        services.AddOptions<DataArchiveOptions>()
            .Bind(LambdaConfiguration.Instance.GetSection(nameof(DataArchiveOptions)))
            .ValidateFluentValidation();
    }

    private static void ConfigureApplicationServices(IServiceCollection services)
    {
        // Get the AWS profile information from configuration providers
        var awsOptions = LambdaConfiguration.Instance.GetAWSOptions();

        // Configure AWS service clients to use these credentials
        services.AddDefaultAWSOptions(awsOptions);

        services.AddSingleton<ITestService, TestService>();
    }
}