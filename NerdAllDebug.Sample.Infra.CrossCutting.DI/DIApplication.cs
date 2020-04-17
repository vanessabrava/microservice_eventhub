using Microsoft.Extensions.DependencyInjection;
using NerdAllDebug.Sample.App.Services;
using NerdAllDebug.Sample.App.Services.Imp;

namespace NerdAllDebug.Sample.Infra.CrossCutting.DI
{
    internal static class DIApplication
    {
        internal static void ConfigureDI(IServiceCollection services)
        {
            services.AddScoped<ISampleAppService, SampleAppService>();
            services.AddScoped<IQueuesApplicationService, QueuesApplicationService>();
        }
    }
}
