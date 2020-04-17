using Microsoft.Extensions.DependencyInjection;

namespace NerdAllDebug.Sample.Host.Api.Configurations
{
    internal static class CorsConfigurations
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services, string corsPolicyName)
        {
            return
                services.AddCors(options =>
                {
                    options.AddPolicy(corsPolicyName,
                      builder => builder.AllowAnyOrigin()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader());
                });
        }
    }
}
