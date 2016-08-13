using MongoDB.Driver;
using RP_Backend.Models.Sentences;
using RP_Backend.Repository;
using RP_Backend.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RP_Backend.Tests
{
    /// <summary>
    /// Sentence service layer and database testing class
    /// </summary> 
    public class SentenceServiceTests
    {

        SentenceService service;

        IMongoCollection<SentenceModel> dbCollection;

        string testSentence = "easter egg";
        string[] tags = new string[] { "easter", "egg" };

        const string defaultMongoUrl = "mongodb://127.0.0.1:27017";

        public SentenceServiceTests()
        {
            //Setup and drop test db
            var mongoDb = new MongoClient(defaultMongoUrl);
            mongoDb.DropDatabase("RPDB-Test");

            var testDb = new DbContext("RPDB-Test");
            dbCollection = testDb.GetCollection<SentenceModel>("Sentences");

            service = new SentenceService(testDb);

            setupDb();
        }

        void setupDb()
        {
            var sentences = new List<SentenceModel>();
            //Add a bunch of sentences
            for (int i = 0; i != 50; i++)
            {
                sentences.Add(new SentenceModel() { Sentence = testSentence, Tags = tags });
            }

            dbCollection.InsertMany(sentences);
        }

        //Test the ability to add a sentence
        [Fact]
        public async Task CanAddValidSentence()
        {
            string differentSentence = "eggs";
            string[] differentTags = new string[] { "eggs" };

            //Create model
            SentenceModel model = new SentenceModel();
            model.Sentence = differentSentence;

            var result = await service.Create(model);

            var builder = Builders<SentenceModel>.Filter;
            var filter = builder.Eq("Sentence", differentSentence);

            Assert.Equal(result.Tags, differentTags);
            Assert.Equal(dbCollection.Find(filter).Count(), 1);
        }

        //Test the ability to not add a blank sentence
        [Fact]
        public void CantAddBlankSentence()
        {
            //Create model
            SentenceModel model = new SentenceModel();
            model.Sentence = "";

            Assert.ThrowsAny<System.Exception>(() => service.Create(model).RunSynchronously());
        }

        //Test the ability to not add a null model
        [Fact]
        public void CantNullSentence()
        {
            //Create model
            SentenceModel model = null;

            Assert.ThrowsAny<System.Exception>(() => service.Create(model).RunSynchronously());
        }

        //Test the ability to retrieve sentences
        [Fact]
        public async Task CanGetSentence()
        {
            var result = await service.Get(0);

            Assert.Equal(result.Sentences.Count(), 20);
        }

        //Test the ability to get sentences on different pages
        [Fact]
        public async Task CanGetSentencesOnDifferentPages()
        {
            var result = await service.Get(1);
            var resultPage1 = await service.Get(0);

            Assert.Equal(result.Pagination.Current, 1);
            Assert.Equal(result.Pagination.Total, 3);
            Assert.NotEqual(result.Sentences, resultPage1.Sentences);
        }

        //Test the ability to delete a sentence by ID
        [Fact]
        public async Task CanDeleteSentence()
        {
            var result = await service.Get(0);
            var deleteSentence = result.Sentences.First();

            await service.Delete(deleteSentence.ID.ToString());

            result = await service.Get(0);

            Assert.Equal(result.Sentences.Where(x => x.ID == deleteSentence.ID).Count(), 0);            
        }

        //Test the ability to run statistics on sentences
        [Fact]
        public async Task CanGetStatistics()
        {
            var result = await service.Statistics();

            Assert.True(result.First(x => x.Tag == tags[0]).Count > 49);
            Assert.Equal(result.Count(), 2);
        }
    }
}