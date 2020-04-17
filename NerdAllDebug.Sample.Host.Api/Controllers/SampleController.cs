using Microsoft.AspNetCore.Mvc;
using NerdAllDebug.Sample.App.Messages;
using NerdAllDebug.Sample.App.Services;
using iBeach.Framework.Services;
using System.Threading.Tasks;

namespace NerdAllDebug.Sample.Host.Api.Controllers
{
    /// <summary>
    /// API with default endpoints to sample.
    /// </summary>
    [Route("api/sample")]
    public class SampleController : ControllerBase
    {
        private ISampleAppService SampleAppService { get; }

        /// <summary>
        /// Defaul constructor;
        /// </summary>
        /// <param name="sampleAppService">Defaul application service.</param>
        public SampleController(ISampleAppService sampleAppService) => SampleAppService = sampleAppService;

        /// <summary>
        /// Insert new sample value.
        /// </summary>
        /// <param name="request">Default request.</param>
        /// <returns>Result of operation.</returns>
        /// <response code="200">Sample created with sucessfuly.</response>
        /// <response code="400">The field 'name' is required.</response>
        /// <response code="401">Unauthorized because the Authorization-Token is invalid.</response>
        /// <response code="422">The name exists.</response>
        /// <response code="500">Error on API.</response>
        [HttpPost, Produces("application/json", Type = typeof(ResultResponseMessage<SampleResponseMessage>))]
        public async Task<IActionResult> New([FromBody] NewSampleRequestMessage request) => await SampleAppService.NewAsync(request);

        /// <summary>
        /// Update sample values.
        /// </summary>
        /// <param name="request">Default request.</param>
        /// <returns>Result of operation.</returns>
        /// <response code="200">Sample updated with sucessfuly.</response>
        /// <response code="400">The field 'name' is required.</response>
        /// <response code="401">Unauthorized because the Authorization-Token is invalid.</response>
        /// <response code="422">The sample not exists or the name exists.</response>>
        /// <response code="500">Error on API.</response>
        [HttpPut("{idSample}"), Produces("application/json", Type = typeof(ResultResponseMessage<SampleResponseMessage>))]
        public async Task<IActionResult> Update(UpdateSampleRequestMessage request) => await SampleAppService.UpdateAsync(request);

        /// <summary>
        /// Delete sample.
        /// </summary>
        /// <param name="request">Default request.</param>
        /// <returns>Result of operation.</returns>
        /// <response code="204">Sample deleted with sucessfuly.</response>
        /// <response code="401">Unauthorized because the Authorization-Token is invalid.</response>
        /// <response code="422">The sample not exists.</response>>
        /// <response code="500">Error on API.</response>
        [HttpDelete("{idSample}"), Produces("application/json", Type = typeof(ResultResponseMessage))]
        public async Task<IActionResult> Delete(DeleteSampleRequestMessage request) => await SampleAppService.DeleteAsync(request);

        /// <summary>
        /// Get one sample.
        /// </summary>
        /// <param name="request">Default request.</param>
        /// <returns>Result of operation.</returns>
        /// <response code="200">Success to get sample.</response>
        /// <response code="204">Not found sample.</response>
        /// <response code="500">Error on API.</response>
        [HttpGet("{idSample}"), Produces("application/json", Type = typeof(ResultResponseMessage<SampleResponseMessage>))]
        public async Task<IActionResult> Get(GetSampleRequestMessage request) => await SampleAppService.GetAsync(request);

        /// <summary>
        /// Get one sample.
        /// </summary>
        /// <param name="request">Default request.</param>
        /// <returns>Result of operation.</returns>
        /// <response code="200">Success to get sample.</response>
        /// <response code="204">Not found sample.</response>
        /// <response code="500">Error on API.</response>
        [HttpGet, Produces("application/json", Type = typeof(ResultResponseMessage<SampleResponseMessage>))]
        public async Task<IActionResult> Get(RequestMessage request) => await SampleAppService.GetAsync(request);
    }
}