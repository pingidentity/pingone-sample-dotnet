using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using PingOne.Core.Management;
using PingOne.Core.Management.Services;

namespace PingOne.Core.Configuration.Extensions
{
    public static class AddManagementExtensions
    {
        public static IServiceCollection AddPingOneManagement(
            this IServiceCollection services,
            PingOneConfigurationManagement configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            PingOneConfigurationValidator.ValidateManagementConfiguration(configuration);

            services.AddSingleton(configuration);
            services.AddSingleton<IMemoryCache, MemoryCache>();

            services.AddHttpClient<IPingOneTokenProvider, PingOneTokenProvider>(
                nameof(IPingOneTokenProvider),
                client =>
                {
                    client.BaseAddress = new Uri($"{configuration.AuthBaseUrl}/{configuration.EnvironmentId}/as/");
                });

            services.AddHttpClient<IManagementApiClient, ManagementApiClient>(
                    nameof(IManagementApiClient),
                    client =>
                    {
                        client.BaseAddress = new Uri($"{configuration.ApiBaseUrl}/v1/environments/{configuration.EnvironmentId}/");
                    })
                .AddHttpMessageHandler<PingOneApiAuthorizationHeaderHandler>();

            services.AddTransient<PingOneApiAuthorizationHeaderHandler>();

            return services;
        }
    }
}
