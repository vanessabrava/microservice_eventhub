using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdAllDebug.Sample.Host.Api.Configurations;
using iBeach.Framework.Host.Api.Configurations;

namespace NerdAllDebug.Sample.Host.Api
{
    /// <summary>
    /// Class to initialize API.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Default contructor.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) => Configuration = configuration;

        /// <summary>
        /// Default configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Default policy name
        /// </summary>
        public static string CorsPolicyName => "SampleCorsPolicy";

        /// <summary>
        /// The swagger title.
        /// </summary>
        public static string SwaggerDocTitle => "Radix - Microservice Sample";

        /// <summary>
        /// the swagger version.
        /// </summary>
        public static string SwaggerDocVersion => "v1";

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .ConfigureAppSettingsOptions(Configuration)
                .ConfigureCors(CorsPolicyName)
                .ConfigureSwagger<Startup>(SwaggerDocTitle, SwaggerDocVersion)
                .ConfigureDI(Configuration)
                .AddControllers(options => options.ConfigureMvcOptions(Configuration));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Defines a class that provides the mechanisms to configure an application's request pipeline.</param>
        /// <param name="env">Provides information about the web hosting environment an application is running in.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerUI(SwaggerDocTitle, SwaggerDocVersion);
            app.UseGlobalization(Configuration);
            app.UseStaticFiles();
            app.UseRouting();
            app.UseMiddlewares<Startup>(env);
            app.UseCors(CorsPolicyName);
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}