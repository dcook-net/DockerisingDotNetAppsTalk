using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MeetupMembers.Model;
using MeetupMembers.Mongo;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NUnit.Framework;

namespace TestProject1
{
    public class IntegrationTests
    {
        private string _mongoUrl;
        private string _serviceUri;

        [SetUp]
        public void Setup()
        {
            _mongoUrl = Environment.GetEnvironmentVariable("MONGOURL");
            _serviceUri = Environment.GetEnvironmentVariable("SERVICEURI");
//            _mongoUrl = "mongodb://localhost:27017";
//            _serviceUri = "http://localhost:9010";
        }

        [Test]
        public async Task ShouldSaveNewMemberToDatabase()
        {
            var serviceUnderTest = new HttpClient
            {
                BaseAddress = new Uri(_serviceUri)
            };

            var newMember = new Member
            {
                FirstName = "Bilbo",
                Surname = "Baggins",
                Bio = "Big man of the shire!"
            };
            
            HttpContent content = new JsonContent(newMember, new JsonSerializerSettings());

            var result = await serviceUnderTest.PostAsync("members", content, CancellationToken.None);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var readAsStringAsync = await result.Content.ReadAsStringAsync();
                Assert.That(readAsStringAsync, Is.EqualTo("Lol"), () => readAsStringAsync);
            }

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(result.Headers.Location, Is.Not.Null);

            var memberId = result.Headers.Location.ToString().Split("/").Last();

            var memberFromDatabase = await GetMemberFromDatabase(memberId);
            
            Assert.That(memberFromDatabase.FirstName, Is.EqualTo(newMember.FirstName));
            Assert.That(memberFromDatabase.Surname, Is.EqualTo(newMember.Surname));
            Assert.That(memberFromDatabase.Bio, Is.EqualTo(newMember.Bio));
        }

        private async Task<Member> GetMemberFromDatabase(string id)
        {
            var mongoConfig = new MongoConfiguration
            {
                MongoConnection = _mongoUrl
            };

            var mongoOptions = Options.Create(mongoConfig);

            var mongoClientBuilder = new MongoClientBuilder();
            var dbProvider = new MongoDatabaseProvider(mongoOptions, mongoClientBuilder);
            var provider = new MongoCollectionProvider<Member>(dbProvider, mongoOptions);
            var repository = new MongoDataRepository<Member>(provider);

            return await repository.Read(id, CancellationToken.None);
        }
    }
}