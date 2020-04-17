using NerdAllDebug.Sample.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdAllDebug.Sample.Infra.DataAccess.Oracle
{
    public class SampleRepository : ISampleRepository
    {
        public static IList<FooSample> FooSamples { get; private set; }

        public async Task NewAsync(FooSample model)
        {
            if (FooSamples == null)
                FooSamples = new List<FooSample>() { model };
            else
                FooSamples.Add(model);

            await Task.CompletedTask;
        }

        public async Task UpdateAsync(FooSample model)
        {
            foreach (var fooSample in FooSamples)
            {
                if (fooSample.IdFooSample == model.IdFooSample)
                    fooSample.Update(model.Name, model.IsQueued);
            }

            await Task.CompletedTask;
        }

        public async Task UpdateAsync(IEnumerable<FooSample> models)
        {
            foreach (var fooSample in FooSamples.Where(it => models.Any(fooSample => fooSample.IdFooSample == it.IdFooSample)))
                fooSample.Update(fooSample.Name, fooSample.IsQueued);

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int idFooSample)
        {
            var fooSample = FooSamples.FirstOrDefault(it => it.IdFooSample == idFooSample);
            FooSamples.Remove(fooSample);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<FooSample>> GetAsync() => await Task.FromResult(FooSamples);

        public async Task<FooSample> GetAsync(Func<FooSample, bool> predicate) => await Task.FromResult(FooSamples?.FirstOrDefault(predicate));

        public async Task<bool> ExistsAsync(string name) => await Task.FromResult(FooSamples != null && FooSamples.Any(it => it.Name.ToLower() == name.ToLower()));

        public async Task<IEnumerable<FooSample>> GetAllAsync(Func<FooSample, bool> predicate)
        {
            var fooSamples = new List<FooSample>();
            foreach (var fooSample in await Task.FromResult(FooSamples.Where(predicate)))
                fooSamples.Add(fooSample);

            return fooSamples;
        }
    }
}
