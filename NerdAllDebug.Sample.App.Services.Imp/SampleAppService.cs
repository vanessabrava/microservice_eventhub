using NerdAllDebug.Sample.App.Mappers;
using NerdAllDebug.Sample.App.Messages;
using NerdAllDebug.Sample.Services;
using NerdAllDebug.Sample.Services.Model;
using iBeach.Framework.Application.Extension;
using iBeach.Framework.Services;
using System.Threading.Tasks;

namespace NerdAllDebug.Sample.App.Services.Imp
{
    public class SampleAppService : ISampleAppService
    {
        private ISampleService SampleService { get; }

        public SampleAppService(ISampleService sampleService) => SampleService = sampleService;

        public async Task<ResultResponseMessage<SampleResponseMessage>> NewAsync(NewSampleRequestMessage request)
        {
            var fooSampleResult = await SampleService.NewAsync(request.Name);

            var result = fooSampleResult.ToResultResponseMessage<NewSampleRequestMessage, SampleResponseMessage, FooSample>(request);

            if (result.IsHttpStatusCodeError())
                return result;

            var response = SampleMessageMapper.ToResponse(fooSampleResult.Model);

            result.SetReturn(response);

            return result;
        }

        public async Task<ResultResponseMessage<SampleResponseMessage>> UpdateAsync(UpdateSampleRequestMessage request)
        {
            var fooSampleResult = await SampleService.UpdateAsync(request.IdSample, request.UpdateSampleMessage.Name);

            var result = fooSampleResult.ToResultResponseMessage<UpdateSampleRequestMessage, SampleResponseMessage, FooSample>(request);

            if (result.IsHttpStatusCodeError())
                return result;

            var response = SampleMessageMapper.ToResponse(fooSampleResult.Model);

            result.SetReturn(response);

            return result;
        }

        public async Task<ResultResponseMessage> DeleteAsync(DeleteSampleRequestMessage request)
        {
            var modelResult = await SampleService.DeleteAsync(request.IdSample);

            var result = modelResult.ToResultResponseMessage(request);

            if (!result.IsHttpStatusCodeError())
                result.CreateResponseNotContent();

            return result;
        }

        public async Task<ResultResponseMessage<SampleResponseMessage>> GetAsync(GetSampleRequestMessage request)
        {
            var fooSampleResult = await SampleService.GetAsync(request.IdSample);

            var result = fooSampleResult.ToResultResponseMessage<GetSampleRequestMessage, SampleResponseMessage, FooSample>(request);

            var response = SampleMessageMapper.ToResponse(fooSampleResult.Model);

            if (response == null)
                result.CreateResponseNotContent();
            else
                result.SetReturn(response);

            return result;
        }

        public async Task<ResultResponseMessage<SamplesResponseMessage>> GetAsync(RequestMessage request)
        {
            var fooSampleResult = await SampleService.GetAsync();

            var result = fooSampleResult.ToResultResponseMessage<RequestMessage, SamplesResponseMessage, FooSample>(request);

            var response = SampleMessageMapper.ToResponse(fooSampleResult.Models);

            if (response == null)
                result.CreateResponseNotContent();
            else
                result.SetReturn(response);

            return result;
        }
    }
}
