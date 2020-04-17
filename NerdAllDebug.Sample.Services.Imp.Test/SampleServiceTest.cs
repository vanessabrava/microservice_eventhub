using Moq;
using NUnit.Framework;
using NerdAllDebug.Sample.Infra.DataAccess;
using NerdAllDebug.Sample.Services.Model;
using iBeach.Framework.Model.ModelRules;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdAllDebug.Sample.Services.Imp.Test
{
    [TestFixture]
    public class SampleServiceTest
    {
        [TestCase("Jhon", true, false, 422, "The sample with name Jhon exists.")]
        [TestCase("Jhon", false, true, 200, "Success.")]
        public async Task NewAsync(string name, bool exits, bool isValid, int code, string message)
        {
            //Arrange
            Mock<ISampleRepository> SampleRepositoryMock = new Mock<ISampleRepository>();
            var sampleService = new SampleService(SampleRepositoryMock.Object);
            SampleRepositoryMock.Setup(it => it.ExistsAsync(It.IsAny<string>())).Returns(Task.FromResult(exits));

            //Act
            var sampleModelResult = await sampleService.NewAsync(name);

            //Assert
            Assert.That(sampleModelResult.IsModelResultValid(), Is.EqualTo(isValid));
            Assert.That(sampleModelResult.Message, Is.EqualTo(message));
            Assert.That(sampleModelResult.ModelStatusCode, Is.EqualTo(code));
        }

        [TestCase("Jhon", false, false, 422, "The sample with idSample 1 not exists.")]
        [TestCase("Jhon", true, false, 422, "The sample with name Jhon exists.")]
        [TestCase("Jhon", false, true, 200, "Success.")]
        public async Task UpdateAsync(string name, bool exits, bool isValid, int code, string message)
        {
            //Arrange
            Mock<ISampleRepository> SampleRepositoryMock = new Mock<ISampleRepository>();
            var sampleService = new SampleService(SampleRepositoryMock.Object);
            SampleRepositoryMock.Setup(it => it.ExistsAsync(It.IsAny<string>())).Returns(Task.FromResult(exits));

            IModelResult<FooSample> newFooSampleResult = FooSample.New(name);

            if ((exits && !isValid) || (!exits && isValid))
                SampleRepositoryMock.Setup(it => it.GetAsync(it=> it.IdFooSample == It.IsAny<int>())).Returns(Task.FromResult(newFooSampleResult.Model));

            //Act
            var sampleModelResult = await sampleService.UpdateAsync(1, name);

            //Assert
            Assert.That(sampleModelResult.IsModelResultValid(), Is.EqualTo(isValid));
            Assert.That(sampleModelResult.Message, Is.EqualTo(message));
            Assert.That(sampleModelResult.ModelStatusCode, Is.EqualTo(code));
        }

        [TestCase(false, true, true)]
        [TestCase(true, true, false)]
        public async Task GetAsync(bool exits, bool isValid, bool model)
        {
            //Arrange
            Mock<ISampleRepository> SampleRepositoryMock = new Mock<ISampleRepository>();
            var sampleService = new SampleService(SampleRepositoryMock.Object);
            ModelResult<FooSample> fooSampleResult = FooSample.New("Jhon");
            SampleRepositoryMock.Setup(it => it.GetAsync(it=> it.IdFooSample == It.IsAny<int>())).Returns(Task.FromResult(exits ? fooSampleResult.Model : default));

            //Act
            var sampleModelResult = await sampleService.GetAsync(It.IsAny<int>());

            //Assert
            Assert.That(sampleModelResult.IsModelResultValid(), Is.EqualTo(isValid));
            Assert.That(sampleModelResult.Model == null, Is.EqualTo(model));
        }

        [TestCase(false, true, true)]
        [TestCase(true, true, false)]
        public async Task GetAllAsync(bool exits, bool isValid, bool model)
        {
            //Arrange
            Mock<ISampleRepository> SampleRepositoryMock = new Mock<ISampleRepository>();
            var sampleService = new SampleService(SampleRepositoryMock.Object);

            ModelResult<FooSample> fooSampleResult1 = FooSample.New("Jhon");
            ModelResult<FooSample> fooSampleResult2 = FooSample.New("Jhon");
            ModelResult<FooSample> fooSampleResult3 = FooSample.New("Jhon");

            IEnumerable<FooSample> fooSamples = new List<FooSample>()
            {
                fooSampleResult1.Model,
                fooSampleResult2.Model,
                fooSampleResult3.Model
            };

            SampleRepositoryMock.Setup(it => it.GetAsync()).Returns(Task.FromResult(exits ? fooSamples : default));

            //Act
            var sampleModelListResult = await sampleService.GetAsync();

            //Assert
            Assert.That(sampleModelListResult.IsModelResultValid(), Is.EqualTo(isValid));
            Assert.That(sampleModelListResult.Models == null, Is.EqualTo(model));
        }
    }
}