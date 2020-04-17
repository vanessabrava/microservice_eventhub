using iBeach.Framework.Services;
using System.Threading.Tasks;

namespace NerdAllDebug.Sample.App.Services
{
    public interface IQueuesApplicationService
    {
        Task<ResultResponseMessage<ResponseMessage>> QueuesAsync(RequestMessage request);
    }
}
