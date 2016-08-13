using Microsoft.AspNetCore.Mvc;
using Moq;
using RP_Backend.Controllers;
using RP_Backend.Models.Sentences;
using RP_Backend.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RP_Backend.Tests
{
    /// <summary>
    /// Sentence Controller (API endpoint) tests
    /// </summary> 
    public class SentenceControllerTests
    {
        SentencesController controller;
        Mock<ISentenceService> service;

        public SentenceControllerTests()
        {
            service = new Mock<ISentenceService>();

            controller = new SentencesController(service.Object);
        }

        //GET api/sentences/statistics
        [Fact]
        public async Task CanGetStatistics()
        {
            service.Setup(x => x.Statistics()).Returns(Task.FromResult<IEnumerable<StatisticsModel>>(new List<StatisticsModel>())).Verifiable();

            await controller.Statistics();

            service.Verify();
        }

        // GET api/sentences
        [Fact]
        public async Task CanGetSentences()
        {
            service.Setup((x) => x.Get(It.IsAny<int>())).Returns(Task.FromResult<SentencesGetReturnType>(new SentencesGetReturnType())).Verifiable();

            await controller.Get(0);

            service.Verify();
        }

        // POST api/sentences
        [Fact]
        public async Task CanAddSentence()
        {
            service.Setup(x => x.Create(It.IsAny<SentenceModel>())).Returns(Task.FromResult<SentenceModel>(new SentenceModel())).Verifiable();

            await controller.Add(new SentenceModel() { Sentence = "test" });

            service.Verify();
        }

        // DELETE api/sentences/{id}
        public async Task CanDeleteSentence()
        {
            service.Setup(x => x.Delete(It.IsAny<string>())).Returns(Task.FromResult<bool>(true)).Verifiable();

            var result = await controller.Delete("testid");

            service.Verify();
            Assert.Equal(result.GetType(), typeof(OkResult));
        }

        // DELETE api/sentences/{id}
        public async Task CannotDeleteSentence()
        {
            service.Setup(x => x.Delete(It.IsAny<string>())).Returns(Task.FromResult<bool>(false)).Verifiable();

            var result = await controller.Delete("testid");

            service.Verify();
            Assert.Equal(result.GetType(), typeof(NotFoundResult));
        }
    }
}
