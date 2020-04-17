using Microsoft.Extensions.DependencyInjection;
using NerdAllDebug.Sample.Services;
using NerdAllDebug.Sample.Services.Imp;

namespace NerdAllDebug.Sample.Infra.CrossCutting.DI
{
    internal static class DIServices
    {
        internal static void ConfigureDI(IServiceCollection services)
        {
            services.AddScoped<ISampleService, SampleService>();
        }
    }
}
