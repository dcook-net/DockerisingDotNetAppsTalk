using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MeetupMembers.Mongo
{
    public class MongoDatabaseProvider : IMongoDatabaseProvider
    {
        private readonly MongoConfiguration _mongoConfig;
        private readonly IMongoClientBuilder _mongoClientBuilder;

        public MongoDatabaseProvider(IOptions<MongoConfiguration> mongoConfig, IMongoClientBuilder mongoClientBuilder)
        {
            _mongoConfig = mongoConfig.Value;
            _mongoClientBuilder = mongoClientBuilder;
        }

        public IMongoDatabase GetDatabase()
        {
            var client = _mongoClientBuilder.Build(_mongoConfig);
            return client.GetDatabase(_mongoConfig.DatabaseName);
        }

        public string MongoUrl => _mongoConfig.MongoUrl.ToString();
    }
}