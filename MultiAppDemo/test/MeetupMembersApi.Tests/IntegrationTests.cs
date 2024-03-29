using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MeetupMembersApi.Models;
using MeetupMembersApi.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MeetupMembersApi.Tests
{
    public class IntegrationTests
    {
        private string _mongoUrl;
        private string _serviceUri;
        private HttpClient _serviceUnderTest;
        private IMongoCollection<Member> _backendDatabase;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mongoUrl = Environment.GetEnvironmentVariable("MONGOURL");
            _serviceUri = Environment.GetEnvironmentVariable("SERVICEURI");

            _serviceUnderTest = new HttpClient
            {
                BaseAddress = new Uri(_serviceUri)
            };
            
            _backendDatabase = GetMongoDataRepository();
        }

        [Test]
        public async Task ShouldSaveNewMemberToDatabase()
        {
            //Arrange
            var newMember = GenerateBrandNewMember();

            //Act
            var result = await _serviceUnderTest.PostAsync("members", JsonContent.Create(newMember), CancellationToken.None);

            //Assert
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(result.Headers.Location, Is.Not.Null);

            var memberId = result.Headers.Location.ToString().Split("/").Last();

            var memberFromDatabase = await _backendDatabase.GetMemberById(memberId);
            
            Assert.That(memberFromDatabase.FirstName, Is.EqualTo(newMember.FirstName));
            Assert.That(memberFromDatabase.Surname, Is.EqualTo(newMember.Surname));
            Assert.That(memberFromDatabase.Bio, Is.EqualTo(newMember.Bio));
        }

        [Test]
        public async Task ShouldRetrieveAllExistingMembers()
        {
            const int numberOfMembers = 5;
            var existingMembers = BuildMembers(numberOfMembers);

            foreach (var existingMember in existingMembers)
            {
                await _backendDatabase.Insert(existingMember);
            }

            var result = await _serviceUnderTest.GetAsync("/members", CancellationToken.None);
            
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var members = await Deserialize<List<Member>>(result);
            
            Assert.That(members.Count, Is.EqualTo(numberOfMembers));
        }

        private IEnumerable<Member> BuildMembers(int x)
        {
            for (var i = 0; i < x; i++)
            {
                yield return GenerateMemberWithId();
            }
        }

        [Test]
        public async Task ShouldRetrieveMemberInfoForExistingMember()
        {
            //Arrange
            var existingMember = GenerateMemberWithId();
            await _backendDatabase.Insert(existingMember);
            
            //Act
            var result = await _serviceUnderTest.GetAsync($"members/{existingMember.Id}", CancellationToken.None);
            
            //Assert
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var retrievedMemberInfo = await Deserialize<Member>(result);

            Assert.That(retrievedMemberInfo.Id, Is.EqualTo(existingMember.Id));
            Assert.That(retrievedMemberInfo.FirstName, Is.EqualTo(existingMember.FirstName));
            Assert.That(retrievedMemberInfo.Surname, Is.EqualTo(existingMember.Surname));
            Assert.That(retrievedMemberInfo.Bio, Is.EqualTo(existingMember.Bio));
        }

        private static async Task<T> Deserialize<T>(HttpResponseMessage result)
        {
            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }

        private Member GenerateBrandNewMember()
        {
            return new Member
            {
                FirstName = _fixture.Create<string>(),
                Surname = _fixture.Create<string>(),
                Bio = _fixture.Create<string>()
            };
        }

        private Member GenerateMemberWithId(string memberId = null)
        {
            return new Member
            {
                Id = memberId ?? _fixture.Create<string>(),
                FirstName = _fixture.Create<string>(),
                Surname = _fixture.Create<string>(),
                Bio = _fixture.Create<string>()
            };
        }

        [Test]
        public async Task ShouldUpdateExistingMember()
        {
            //Arrange
            var existingMember = GenerateMemberWithId();
            await _backendDatabase.Insert(existingMember);

            var updatedMemberDetails = GenerateMemberWithId(existingMember.Id);
            
            //Act
            var result = await _serviceUnderTest.PutAsync("members", 
                JsonContent.Create(updatedMemberDetails), CancellationToken.None);
            
            //Assert
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var memberDetailsInDb = await _backendDatabase.GetMemberById(updatedMemberDetails.Id);
            
            Assert.That(memberDetailsInDb.FirstName, Is.EqualTo(updatedMemberDetails.FirstName));
            Assert.That(memberDetailsInDb.Surname, Is.EqualTo(updatedMemberDetails.Surname));
            Assert.That(memberDetailsInDb.Bio, Is.EqualTo(updatedMemberDetails.Bio));
        }

        [Test]
        public async Task ShouldDeleteExistingMember()
        {
            //Arrange
            var memberToDelete = GenerateMemberWithId();
            await _backendDatabase.Insert(memberToDelete);

            //Act
            var result = await _serviceUnderTest.DeleteAsync($"members/{memberToDelete.Id}", CancellationToken.None);

            //Assert
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var member = await _backendDatabase.GetMemberById(memberToDelete.Id);
            
            Assert.That(member, Is.Null);
        }

        private IMongoCollection<Member> GetMongoDataRepository()
        {
            var mongoConfig = new MongoConfiguration
            {
                MongoConnection = _mongoUrl
            };

            var mongoOptions = Options.Create(mongoConfig);

            var mongoClientBuilder = new MongoClientBuilder();
            var dbProvider = new MongoDatabaseProvider(mongoOptions, mongoClientBuilder);
            
            var provider = new MongoCollectionProvider<Member>(dbProvider, mongoOptions);

            return provider.GetCollection();
        }

        [TearDown]
        public void TearDown()
        {
            _backendDatabase.Database.DropCollectionAsync("Members");
        }
    }
}