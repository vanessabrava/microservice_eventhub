using NerdAllDebug.Sample.App.Messages;
using iBeach.Framework.Services;
using System.Threading.Tasks;

namespace NerdAllDebug.Sample.App.Services
{
    public interface ISampleAppService
    {
        Task<ResultResponseMessage<SampleResponseMessage>> NewAsync(NewSampleRequestMessage request);

        Task<ResultResponseMessage<SampleResponseMessage>> UpdateAsync(UpdateSampleRequestMessage request);

        Task<ResultResponseMessage> DeleteAsync(DeleteSampleRequestMessage request);

        Task<ResultResponseMessage<SampleResponseMessage>> GetAsync(GetSampleRequestMessage request);

        Task<ResultResponseMessage<SamplesResponseMessage>> GetAsync(RequestMessage request);
    }
}
