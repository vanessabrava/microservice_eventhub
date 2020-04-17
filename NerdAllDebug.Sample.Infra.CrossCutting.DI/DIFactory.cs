using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NerdAllDebug.Sample.Infra.CrossCutting.DI
{
    public static class DIFactory
    {
        public static void ConfigureDI(IServiceCollection services)
        {
            DIApplication.ConfigureDI(services);
            DIServices.ConfigureDI(services);
            DIData.ConfigureDI(services);
        }
    }
}
