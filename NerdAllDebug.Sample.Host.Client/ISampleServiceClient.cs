using NerdAllDebug.Sample.App.Messages;
using SotreqLink.Framework.Services;
using System.Threading.Tasks;

namespace NerdAllDebug.Sample.Host.Client
{
    /// <summary>
    /// Interface with methods to HTTP calls on Sample Microservice.
    /// <note type="note">To use this client, see example on <see cref="SampleServiceClient"/>.</note>
    /// </summary>
    public interface ISampleServiceClient
    {
        /// <summary>
        /// Asynchronous call to insert new sample value.
        /// </summary>
        /// <param name="request">Default request.</param>
        /// <returns>Result of operation.</returns>
        Task<ResultResponseMessage<SampleResponseMessage>> NewAsync(NewSampleRequestMessage request);

        /// <summary>
        /// Asynchronous call to update sample values.
        /// </summary>
        /// <param name="request">Default request.</param>
        Task<ResultResponseMessage<SampleResponseMessage>> UpdateAsync(UpdateSampleRequestMessage request);

        /// <summary>
        /// Asynchronous call to delete the sample entry.
        /// </summary>
        /// <param name="request">Request with values to delete.</param>
        /// <returns>Result of operation.</returns>
        Task<ResultResponseMessage> DeleteAsync(DeleteSampleRequestMessage request);

        /// <summary>
        /// Asynchronous call to get one sample.
        /// </summary>
        /// <param name="request">Default request.</param>
        /// <returns>Result of operation.</returns>
        Task<ResultResponseMessage<SampleResponseMessage>> GetAsync(GetSampleRequestMessage request);

        /// <summary>
        /// Asynchronous call to get all samples.
        /// </summary>
        /// <param name="request">Default request.</param>
        /// <returns>Result of operation.</returns>
        Task<ResultResponseMessage<SamplesResponseMessage>> GetAsync(RequestMessage request);

        /// <summary>
        /// Asynchronous call to send all samples to queue.
        /// </summary>
        /// <param name="request">Default request.</param>
        /// <returns>Result of operation.</returns>
        Task<ResultResponseMessage<ResponseMessage>> QueuesAsync(RequestMessage request);

    }
}
