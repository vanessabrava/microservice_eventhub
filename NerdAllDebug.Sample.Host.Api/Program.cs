using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NerdAllDebug.Sample.Host.Api.Configurations;
using System.IO;
using HostBuilder = Microsoft.Extensions.Hosting.Host;

namespace NerdAllDebug.Sample.Host.Api
{
    /// <summary>
    /// Program class to host configure.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        protected Program() { }

        /// <summary>
        /// Main method to start configuration.
        /// </summary>
        /// <param name="args">The arguments of configuration.</param>
        public static void Main(string[] args) => CreateHostBuilder(args).Run();


        /// <summary>
        /// The method to create and build the host.
        /// </summary>
        /// <param name="args">The arguments of configuration.</param>
        /// <returns></returns>
        public static IHost CreateHostBuilder(string[] args) =>
            HostBuilder
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder


                //**********This configuration is only Linux*********
                .UseKestrel(options =>
                {
                    options.Limits.MaxRequestBodySize = null;
                    options.Limits.MinResponseDataRate = null;
                })
                //***************************************************

                //***********This configuration is only IIS**********
                //.UseContentRoot(Directory.GetCurrentDirectory())
                //.UseIISIntegration()
                //***************************************************

                .ConfigureAppConfiguration((hostingContext, configuration) => hostingContext.ConfigAppSettingsFiles(configuration))
                .UseStartup<Startup>();
            })
            .Build();
    }
}
