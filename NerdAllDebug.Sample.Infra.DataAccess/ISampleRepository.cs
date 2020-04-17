using NerdAllDebug.Sample.Services.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdAllDebug.Sample.Infra.DataAccess
{
    public interface ISampleRepository
    {
        Task NewAsync(FooSample model);

        Task UpdateAsync(FooSample model);

        Task UpdateAsync(IEnumerable<FooSample> models);

        Task DeleteAsync(int idFooSample);

        Task<IEnumerable<FooSample>> GetAsync();

        Task<FooSample> GetAsync(Func<FooSample, bool> predicate);

        Task<IEnumerable<FooSample>> GetAllAsync(Func<FooSample, bool> predicate);

        Task<bool> ExistsAsync(string name);
    }
}
