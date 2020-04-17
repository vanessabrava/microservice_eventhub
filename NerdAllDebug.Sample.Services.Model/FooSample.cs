using iBeach.Framework.Model;
using iBeach.Framework.Model.ModelRules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdAllDebug.Sample.Services.Model
{
    public class FooSample : IAggregateRoot
    {
        public static IList<int> UsedIds { get; private set; }

        private FooSample() { }

        public int IdFooSample { get; private set; }

        public string Name { get; private set; }

        public bool IsQueued { get; private set; }

        public DateTime DateOfLastModified { get; private set; } = DateTime.Now.ToUniversalTime();

        public static ModelResult<FooSample> New(string name)
        {
            var result = Validate(name);

            if (!result.IsModelResultValid())
                return result;

            var fooSample = new FooSample
            {
                IdFooSample = GetNewId(),
                Name = name 
            };

            result.SetModel(fooSample);

            return result;
        }

        public ModelResult<FooSample> Update(string name, bool isQueued)
        {
            var result = Validate(name);

            if (!result.IsModelResultValid())
                return result;

            Name = name;
            IsQueued = isQueued;
            DateOfLastModified = DateTime.Now.ToUniversalTime();

            result.SetModel(this);

            return result;
        }

        public void SetToQueued() => IsQueued = true;

        public void UndoToQueued() => IsQueued = false;

        private static ModelResult<FooSample> Validate(string name)
        {
            var result = new ModelResult<FooSample>();

            if (string.IsNullOrWhiteSpace(name))
                result.AddValidation("Name", "The field is required.");

            return result;
        }

        private static int GetNewId()
        {
            if (UsedIds!= null && UsedIds.Any())
            {
                var idFooSample = UsedIds.OrderBy(it => it).LastOrDefault();

                idFooSample = ++idFooSample;
                UsedIds.Add(idFooSample);
                return idFooSample;
            }
            
            UsedIds = new List<int>() { 1 };
            return 1;
        }
    }
}
