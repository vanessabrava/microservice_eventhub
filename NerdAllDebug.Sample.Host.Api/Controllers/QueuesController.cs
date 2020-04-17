using Microsoft.AspNetCore.Mvc;
using NerdAllDebug.Sample.App.Services;
using iBeach.Framework.Services;
using System.Threading.Tasks;

namespace NerdAllDebug.Sample.Host.Api.Controllers
{
    /// <summary>
    /// API with default endpoints to send a sample to queue.
    /// </summary>
    [Route("api/queues")]
    public class QueuesController : ControllerBase
    {
        private IQueuesApplicationService QueuesApplicationService { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="queuesApplicationService">Application service injection.</param>
        public QueuesController(IQueuesApplicationService queuesApplicationService) => QueuesApplicationService = queuesApplicationService;

        /// <summary>
        /// Send all samples to queue.
        /// </summary>
        /// <param name="request">Default request.</param>
        /// <returns>Result of operation.</returns>
        /// <response code="202">Accepted and send to queue.</response>
        /// <response code="401">Unauthorized because the Authorization-Token is invalid.</response>
        /// <response code="500">Error on API.</response>
        [HttpPost, Produces("application/json", Type = typeof(ResultResponseMessage<ResponseMessage>))]
        public async Task<IActionResult> Queues([FromBody] RequestMessage request) => await QueuesApplicationService.QueuesAsync(request);
    }
}