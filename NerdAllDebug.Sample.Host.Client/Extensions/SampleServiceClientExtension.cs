using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdAllDebug.Sample.Host.Client.Options;
using SotreqLink.Framework.Services.Client;
using SotreqLink.Framework.Services.Client.Extension;
using System;
using System.Net;
using System.Net.Http;

namespace NerdAllDebug.Sample.Host.Client.Extensions
{
    /// <summary>
    /// Extensions for Sample Service Client.
    /// </summary>
    public static class SampleServiceClientExtension
    {
        /// <summary>
        /// This is an extension method to encapsulate all dependencies injection and configurations of Service Client.
        /// <note type="note">To use this extension, see the example below.</note>
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <param name="appSettingsSampleServiceClientSection">Starts name section."</param>
        /// <returns></returns>
        /// <example>
        /// Representation on appsettings.json file.
        /// <code>
        /// {
        ///   "Clients": {
        ///     "SampleServiceClient": {
        ///       "Name": "SampleServiceClient",
        ///       "AuthorizationToken": "c3d5f8de7c4744b39c532075df0d5dd8",
        ///       "BaseAddress": "https://domain-of-api.com/"
        ///     }
        ///   }
        /// }
        /// </code>
        /// Dependency injection on application.
        /// <code>
        /// public class DIFactory
        /// {
        ///     public static void ConfigureDI(IServiceCollection services, IConfiguration configuration)
        ///     {
        ///         services.AddSampleServiceClient(configuration);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static IServiceCollection AddSampleServiceClient(this IServiceCollection services, IConfiguration configuration, string appSettingsSampleServiceClientSection = "Clients:SampleServiceClient")
        {
            var sampleServiceClientOptions = new SampleServiceClientOptions();

            configuration.GetSection(appSettingsSampleServiceClientSection).Bind(sampleServiceClientOptions);

            if (sampleServiceClientOptions == null) throw new ArgumentException("Interchange Data Service Client configuration must be defined.", "appSettingsInterchangeDataServiceClientSection");

            if (string.IsNullOrWhiteSpace(sampleServiceClientOptions.Name) || 
                string.IsNullOrWhiteSpace(sampleServiceClientOptions.BaseAddress) || 
                string.IsNullOrWhiteSpace(sampleServiceClientOptions.AuthorizationToken))
                throw new ArgumentException("These parameters are required.", "Name|BaseAddress|AuthorizationToken");

            services.AddOptions();
            services.Configure<SampleServiceClientOptions>(options => configuration.GetSection(appSettingsSampleServiceClientSection).Bind(options));
            services.AddHttpRestServiceClientLog(configuration);
            services.AddScoped<ISampleServiceClient, SampleServiceClient>();
            services.AddScoped<IHttpRestServiceClient, HttpRestServiceClient<SampleServiceClient>>();
            services.AddHttpClient(sampleServiceClientOptions.Name, it => it.BaseAddress = new Uri(sampleServiceClientOptions.BaseAddress))
                   .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                   {
                       AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                   });

            return services;
        }
    }
}
