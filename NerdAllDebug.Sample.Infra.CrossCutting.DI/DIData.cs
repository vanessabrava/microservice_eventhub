using Microsoft.Extensions.DependencyInjection;
using NerdAllDebug.Sample.Infra.DataAccess;
using NerdAllDebug.Sample.Infra.DataAccess.Oracle;

namespace NerdAllDebug.Sample.Infra.CrossCutting.DI
{
    internal static class DIData
    {
        internal static void ConfigureDI(IServiceCollection services)
        {
            services.AddScoped<ISampleRepository, SampleRepository>();
        }
    }
}
