using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdAllDebug.Sample.Infra.CrossCutting.DI;
using iBeach.Framework.EventHub.Extensions;
using iBeach.Framework.Host.Api.DependencyInjection;

namespace NerdAllDebug.Sample.Host.Api.Configurations
{
    internal static class DependencyInjectionConfigurations
    {
        public static IServiceCollection ConfigureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostApiLog(configuration);
            services.AddEventHub(configuration);

            DIFactory.ConfigureDI(services);

            return services;
        }
    }
}