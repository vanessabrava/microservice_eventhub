using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NerdAllDebug.Sample.App.Messages;
using NerdAllDebug.Sample.Host.Client.Base;
using NerdAllDebug.Sample.Host.Client.Options;
using iBeach.Framework.Common.Constants;
using iBeach.Framework.Services;
using iBeach.Framework.Services.Client;
using System.Threading.Tasks;

namespace NerdAllDebug.Sample.Host.Client
{
    /// <summary>
    /// Concrete class that implement HTTP calls on Sample Microservice.
    /// </summary>
    public class SampleServiceClient : SampleServiceClientBase, ISampleServiceClient
    {
        private static string SampleUri => "api/sample";

        private static string SampleQueueUri => "api/queue";

        private static string SampleWithIdSampleUri => "api/sample/{idSample}";

        private string ApplicationName { get; }

        /// <summary>
        /// Default constructor to set options and HttpRestServiceClient.
        /// </summary>
        /// <param name="options">The options design pattern used in SampleServiceClient.</param>
        /// <param name="httpRestServiceClient">Interface with the default methods and properties to be used for all HTTP Rest calls.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <example>
        /// Finally, follow the example of how to use the client.
        /// <code>
        /// public class Bar
        /// {
        ///     private ISampleServiceClient SampleServiceClient { get; }
        /// 
        ///     public Bar(ISampleServiceClient sampleServiceClient) => SampleServiceClient = sampleServiceClient;
        /// 
        ///     public async Task NewAsync(string name)
        ///     {
        ///         var protocol = Guid.NewGuid().ToString("N");
        ///         var request = new NewSampleRequestMessage
        ///         {
        ///             Name = "Jhon"
        ///         }
        ///         
        ///         request.AddHeader(Headers.Protocol, protocol);
        /// 
        ///         var result = await SampleServiceClient.NewAsync(request);
        ///     }
        /// }
        /// </code>
        /// </example>
        public SampleServiceClient(IOptionsSnapshot<SampleServiceClientOptions> options, IHttpRestServiceClient httpRestServiceClient, IConfiguration configuration)
            : base(options, httpRestServiceClient)
        => ApplicationName = configuration.GetSection("Host:ApplicationName")?.Value;

        /// <summary>
        /// Asynchronous call to delete the sample entry.
        /// </summary>
        /// <param name="request">Request with values to delete.</param>
        /// <returns>Result of operation.</returns>
        public async Task<ResultResponseMessage> DeleteAsync(DeleteSampleRequestMessage request)
        {
            request.AddHeader(Headers.AuthorizationToken, AuthorizationToken);

            if (!string.IsNullOrWhiteSpace(ApplicationName))
                request.AddHeader(Headers.OriginApplication, ApplicationName);

            return await HttpRestServiceClient.DeleteAsync(Options?.Value?.Name, SampleWithIdSampleUri, request);
        }

        /// <summary>
        /// Asynchronous call to get one sample.
        /// </summary>
        /// <param name="request">Default request.</param>
        /// <returns>Result of operation.</returns> 
        public async Task<ResultResponseMessage<SampleResponseMessage>> GetAsync(GetSampleRequestMessage request)
        {
            request.AddHeader(Headers.AuthorizationToken, AuthorizationToken);

            if (!string.IsNullOrWhiteSpace(ApplicationName))
                request.AddHeader(Headers.OriginApplication, ApplicationName);

            return await HttpRestServiceClient.GetAsync<GetSampleRequestMessage, SampleResponseMessage>(Options?.Value?.Name, SampleWithIdSampleUri, request);
        }

        /// <summary>
        /// Asynchronous call to get all samples.
        /// </summary>
        /// <param name="request">Default request.</param>
        /// <returns>Result of operation.</returns>
        public async Task<ResultResponseMessage<SamplesResponseMessage>> GetAsync(RequestMessage request)
        {
            request.AddHeader(Headers.AuthorizationToken, AuthorizationToken);

            if (!string.IsNullOrWhiteSpace(ApplicationName))
                request.AddHeader(Headers.OriginApplication, ApplicationName);

            return await HttpRestServiceClient.GetAsync<RequestMessage, SamplesResponseMessage>(Options?.Value?.Name, SampleUri, request);
        }

        /// <summary>
        /// Asynchronous call to insert new sample value.
        /// </summary>
        /// <param name="request">Default request.</param>
        /// <returns>Result of operation.</returns>
        public async Task<ResultResponseMessage<SampleResponseMessage>> NewAsync(NewSampleRequestMessage request)
        {
            request.AddHeader(Headers.AuthorizationToken, AuthorizationToken);

            if (!string.IsNullOrWhiteSpace(ApplicationName))
                request.AddHeader(Headers.OriginApplication, ApplicationName);

            return await HttpRestServiceClient.PostAsync<NewSampleRequestMessage, SampleResponseMessage>(Options?.Value?.Name, SampleUri, request);
        }

        /// <summary>
        /// Asynchronous call to update sample values.
        /// </summary>
        /// <param name="request">Default request.</param>
        public async Task<ResultResponseMessage<SampleResponseMessage>> UpdateAsync(UpdateSampleRequestMessage request)
        {
            request.AddHeader(Headers.AuthorizationToken, AuthorizationToken);

            if (!string.IsNullOrWhiteSpace(ApplicationName))
                request.AddHeader(Headers.OriginApplication, ApplicationName);

            return await HttpRestServiceClient.PostAsync<UpdateSampleRequestMessage, SampleResponseMessage>(Options?.Value?.Name, SampleUri, request);
        }

        /// <summary>
        /// Asynchronous call to send all samples to queue.
        /// </summary>
        /// <param name="request">Default request.</param>
        /// <returns>Result of operation.</returns>
        public async Task<ResultResponseMessage<ResponseMessage>> QueuesAsync(RequestMessage request)
        {
            request.AddHeader(Headers.AuthorizationToken, AuthorizationToken);

            if (!string.IsNullOrWhiteSpace(ApplicationName))
                request.AddHeader(Headers.OriginApplication, ApplicationName);

            return await HttpRestServiceClient.PostAsync<RequestMessage, ResponseMessage>(Options?.Value?.Name, SampleQueueUri, request);
        }
    }
}
