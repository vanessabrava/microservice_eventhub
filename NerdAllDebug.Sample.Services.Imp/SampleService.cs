using NerdAllDebug.Sample.Infra.DataAccess;
using NerdAllDebug.Sample.Services.Model;
using iBeach.Framework.Model.ModelRules;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdAllDebug.Sample.Services.Imp
{
    public class SampleService : ISampleService
    {
        private ISampleRepository SampleRepository { get; }

        public SampleService(ISampleRepository sampleRepository) => SampleRepository = sampleRepository;

        public async Task<IModelResult<FooSample>> NewAsync(string name)
        {
            var fooSampleResult = FooSample.New(name);

            if (fooSampleResult.IsModelResultValid())
            {
                var exists = await SampleRepository.ExistsAsync(name);

                if (exists)
                {
                    fooSampleResult.CreateBusinessValidationResult($"The sample with name {name} exists.");
                    return fooSampleResult;
                }

                await SampleRepository.NewAsync(fooSampleResult.Model);
            }

            return fooSampleResult;
        }

        public async Task<IModelResult<FooSample>> UpdateAsync(int idFooSample, string name)
        {
            var fooSampleResult = new ModelResult<FooSample>();
            var fooSample = await SampleRepository.GetAsync(it => it.IdFooSample == idFooSample);

            if (fooSample == null)
            {
                fooSampleResult.CreateBusinessValidationResult($"The sample with idSample {idFooSample} not exists.");
                return fooSampleResult;
            }
            else
            {
                var exists = await SampleRepository.ExistsAsync(name);

                if (exists)
                {
                    fooSampleResult.CreateBusinessValidationResult($"The sample with name {name} exists.");
                    return fooSampleResult;
                }
            }

            fooSampleResult = fooSample.Update(name, fooSample.IsQueued);

            if (!fooSampleResult.IsModelResultValid())
                return fooSampleResult;

            await SampleRepository.UpdateAsync(fooSampleResult.Model);

            return fooSampleResult;
        }

        public async Task<IModelResult<FooSample>> GetAsync(int idFooSample)
        {
            var fooSampleResult = new ModelResult<FooSample>();

            var fooSample = await SampleRepository.GetAsync(it => it.IdFooSample == idFooSample);
            fooSampleResult.SetModel(fooSample);

            return fooSampleResult;
        }

        public async Task<IModelListResult<FooSample>> GetAsync()
        {
            var fooSampleResult = new ModelListResult<FooSample>();

            var fooSamples = await SampleRepository.GetAsync();
            fooSampleResult.SetModel(fooSamples);

            return fooSampleResult;
        }

        public async Task<IModelListResult<FooSample>> GetToSendQueueAsync()
        {
            var fooSampleResult = new ModelListResult<FooSample>();

            var fooSamples = await SampleRepository.GetAllAsync(it => !it.IsQueued);

            foreach (var fooSample in fooSamples)
                fooSample.SetToQueued();

            await SampleRepository.UpdateAsync(fooSamples);

            fooSampleResult.SetModel(fooSamples);

            return fooSampleResult;
        }

        public async Task<IModelResult> UndoSendQueueAsync(IEnumerable<int> idsFooSamples)
        {
            var modelResult = new ModelResult();

            var fooSamples = await SampleRepository.GetAllAsync(it => idsFooSamples.Any(idFooSample => it.IdFooSample == idFooSample));

            foreach (var fooSample in fooSamples)
                fooSample.UndoToQueued();

            await SampleRepository.UpdateAsync(fooSamples);

            return modelResult;
        }

        public async Task<IModelResult> DeleteAsync(int idFooSample)
        {
            var modelResult = new ModelResult();

            var fooSample = await SampleRepository.GetAsync(it => it.IdFooSample == idFooSample);

            if (fooSample == null)
                modelResult.CreateBusinessValidationResult($"The FooSample with id {idFooSample} not found.");
            else
                await SampleRepository.DeleteAsync(idFooSample);

            return modelResult;
        }
    }
}
