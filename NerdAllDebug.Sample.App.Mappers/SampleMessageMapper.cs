using NerdAllDebug.Sample.App.Messages;
using NerdAllDebug.Sample.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdAllDebug.Sample.App.Mappers
{
    public static class SampleMessageMapper
    {

        public static SampleResponseMessage ToResponse(FooSample model)
        {
            if (model == null)
                return null;

            return new SampleResponseMessage { Sample = FromModel(model) };
        }

        public static SamplesResponseMessage ToResponse(IEnumerable<FooSample> models)
        {
            if (models == null || !models.Any())
                return null;

            return new SamplesResponseMessage
            {
                Samples = FromModel(models)
            };
        }

        private static SampleMessage FromModel(FooSample model)
        {
            if (model == null)
                return null;

            return new SampleMessage
            {
                DateOfLastModified = model.DateOfLastModified,
                IdSample = model.IdFooSample,
                Name = model.Name
            };
        }

        public static IEnumerable<SampleMessage> FromModel(IEnumerable<FooSample> models)
        {
            if (models == null || !models.Any())
                return null;

            return models.Select(FromModel);
        }
    }
}
