using iBeach.Framework.Common.Constants;
using iBeach.Framework.EventHub.Services.Producer;
using iBeach.Framework.Services;
using NerdAllDebug.Sample.App.Mappers;
using NerdAllDebug.Sample.App.Messages;
using NerdAllDebug.Sample.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NerdAllDebug.Sample.App.Services.Imp
{
    public class QueuesApplicationService : IQueuesApplicationService
    {
        private string EventHubName => "SampleEventHub";

        private IEventHubProducerService EventHubProducerService { get; }

        private ISampleService SampleService { get; }

        private int DegreeOfParallelism { get; } = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.85) * 2.0));

        public QueuesApplicationService(ISampleService sampleService, IEventHubProducerService eventHubProducerService)
        {
            SampleService = sampleService;
            EventHubProducerService = eventHubProducerService;
        }

        public async Task<ResultResponseMessage<ResponseMessage>> QueuesAsync(RequestMessage request)
        {
            IEnumerable<SampleMessage> samplesMessages = new List<SampleMessage>();

            try
            {
                var samplesModelListResult = await SampleService.GetToSendQueueAsync();
                samplesMessages = SampleMessageMapper.FromModel(samplesModelListResult.Models);

                if (samplesMessages.Any())
                    await SendToQueue(samplesMessages, request.GetHeader(Headers.Protocol));

                var result = new ResultResponseMessage<ResponseMessage>(request);
                var responseMessage = new ResponseMessage();

                responseMessage.AddHeader("Queued-Total", samplesMessages.Count().ToString());
                result.SetReturn(responseMessage);
                result.CreateResponseAccepted();
                return result;
            }
            catch
            {
                await SampleService.UndoSendQueueAsync(samplesMessages.Select(it => it.IdSample));
                throw;
            }
        }

        private async Task SendToQueue(IEnumerable<SampleMessage> samplesMessages, string protocol)
        {
            using var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            using SemaphoreSlim concurrencySemaphore = new SemaphoreSlim(DegreeOfParallelism, DegreeOfParallelism);
            var tasks = new List<Task>();

            var headers = new ConcurrentDictionary<string, object>(new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>(Headers.Protocol, protocol) });

            var pages = samplesMessages.Count() / 100;

            for (int page = 0; page < pages + 1; page++)
            {
                var importMessagePaged = samplesMessages.Skip(100 * page).Take(100);
                var eventSendMessages = importMessagePaged.Select(it => new EventSendMessage<SampleMessage>(it, headers));

                if (importMessagePaged.Any())
                {
                    await concurrencySemaphore.WaitAsync();

                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            await EventHubProducerService.SendEventObjectMessageAsync<SampleMessage>(EventHubName, eventSendMessages);
                        }
                        catch
                        {
                            tokenSource.Cancel();
                            throw;
                        }

                    }).ContinueWith(
                         it =>
                         {
                             if (it.IsFaulted) throw it.Exception;
                             if (it.IsCompleted) concurrencySemaphore.Release();
                         }));
                }
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}