using Microsoft.Extensions.Options;
using NerdAllDebug.Sample.Host.Client.Options;
using iBeach.Framework.Services.Client;
using System;

namespace NerdAllDebug.Sample.Host.Client.Base
{
    /// <summary>
    /// Base class with default configurations for all services of microservice sample.
    /// </summary>
    public class SampleServiceClientBase
    {
        /// <summary>
        /// Interface with the default methods and properties to be used for all HTTP Rest calls.
        /// </summary>
        /// <value>Instance of HttpRestServiceClient</value>
        protected IHttpRestServiceClient HttpRestServiceClient { get; }

        /// <summary>
        /// Key to authorize request on Microservices.
        /// </summary>
        /// <value>c3d5f8de7c4744b39c532075df0d5dd8</value>
        protected string AuthorizationToken { get; }

        /// <summary>
        /// The options design pattern used in SampleServiceClient.
        /// </summary>
        /// <value>Instance of SampleServiceClientOptions</value>
        protected IOptionsSnapshot<SampleServiceClientOptions> Options { get; }

        /// <summary>
        /// Default constructor base to set options and HttpRestServiceClient.
        /// </summary>
        /// <param name="options">The options design pattern used in SampleServiceClient.</param>
        /// <param name="httpRestServiceClient">Interface with the default methods and properties to be used for all HTTP Rest calls.</param>
        protected SampleServiceClientBase(IOptionsSnapshot<SampleServiceClientOptions> options, IHttpRestServiceClient httpRestServiceClient)
        {
            HttpRestServiceClient = httpRestServiceClient;
            Options = options;

            if (string.IsNullOrWhiteSpace(options.Value?.AuthorizationToken))
                throw new ArgumentNullException("SampleServiceClientOptions.AuthorizationToken", "The token of authorization cannot be null.");

            if (string.IsNullOrWhiteSpace(options.Value?.BaseAddress))
                throw new ArgumentNullException("SampleServiceClientOptions.BaseAddress", "The base address cannot be null.");

            if (string.IsNullOrWhiteSpace(options.Value?.Name))
                throw new ArgumentNullException("SampleServiceClientOptions.Name", "The name of client cannot be null.");

            AuthorizationToken = options.Value.AuthorizationToken;
        }
    }
}