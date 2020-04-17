using Microsoft.Extensions.DependencyInjection;
using NerdAllDebug.Sample.Infra.ServiceProvider;
using NerdAllDebug.Sample.Infra.ServiceProvider.Xml;

namespace NerdAllDebug.Sample.Infra.CrossCutting.DI
{
    internal static class DIServiceProvider
    {
        internal static void ConfigureDI(IServiceCollection services)
        {
            services.AddScoped<IVisionLinkDiagnosticServiceProvider, VisionLinkDiagnosticServiceProvider>();
        }
    }
}
