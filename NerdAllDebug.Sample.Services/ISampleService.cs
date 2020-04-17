using NerdAllDebug.Sample.Services.Model;
using iBeach.Framework.Model.ModelRules;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdAllDebug.Sample.Services
{
    public interface ISampleService
    {
        Task<IModelResult<FooSample>> NewAsync(string name);

        Task<IModelResult<FooSample>> UpdateAsync(int idFooSample, string name);

        Task<IModelResult<FooSample>> GetAsync(int idFooSample);

        Task<IModelListResult<FooSample>> GetAsync();

        Task<IModelResult> DeleteAsync(int idFooSample);
        
        Task<IModelListResult<FooSample>> GetToSendQueueAsync();

        Task<IModelResult> UndoSendQueueAsync(IEnumerable<int> idsFooSamples);
    }
}
