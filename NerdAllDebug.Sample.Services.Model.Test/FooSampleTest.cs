using NUnit.Framework;
using iBeach.Framework.Model.ModelRules;
using System.Threading.Tasks;

namespace NerdAllDebug.Sample.Services.Model.Test
{
    [TestFixture]
    public class FooSampleTest
    {
        [TestCase("", false, 422, "Validation.", true)]
        [TestCase("Jhon", true, 200, "Success.", false)]
        public async Task New(string name, bool isValid, int code, string message, bool model)
        {
            //Arrange

            //Act
            IModelResult<FooSample> fooSampleResult = FooSample.New(name);

            //Assert
            Assert.That(fooSampleResult.IsModelResultValid(), Is.EqualTo(isValid));
            Assert.That(fooSampleResult.Message, Is.EqualTo(message));
            Assert.That(fooSampleResult.ModelStatusCode, Is.EqualTo(code));
            Assert.That(fooSampleResult.Model == null, Is.EqualTo(model));

            await Task.CompletedTask;
        }

        [TestCase("Jhon", "", false, 422, "Validation.", true)]
        [TestCase("Jhon", "Jhon Albert", true, 200, "Success.", false)]
        public async Task Update(string name, string updateName, bool isValid, int code, string message, bool model)
        {
            //Arrange

            //Act
            IModelResult<FooSample> fooSampleResult = FooSample.New(name);
            var fooSampleUpdateResult = fooSampleResult.Model.Update(updateName, true);

            //Assert
            Assert.That(fooSampleUpdateResult.IsModelResultValid(), Is.EqualTo(isValid));
            Assert.That(fooSampleUpdateResult.Message, Is.EqualTo(message));
            Assert.That(fooSampleUpdateResult.ModelStatusCode, Is.EqualTo(code));
            Assert.That(fooSampleUpdateResult.Model == null, Is.EqualTo(model));

            await Task.CompletedTask;
        }
    }
}