using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MeetupMembersApi.Mongo
{
    public class MongoCollectionProvider<T> : IMongoCollectionProvider<T>
    {
        private readonly IMongoDatabaseProvider _mongoDatabaseProvider;
        private readonly MongoConfiguration _mongoConfig;

        public MongoCollectionProvider(IMongoDatabaseProvider mongoDatabaseProvider, IOptions<MongoConfiguration> mongoConfig)
        {
            _mongoDatabaseProvider = mongoDatabaseProvider;
            _mongoConfig = mongoConfig.Value;
        }

        public IMongoCollection<T> GetCollection()
        {
            return _mongoDatabaseProvider.GetDatabase().GetCollection<T>(_mongoConfig.CollectionName);
        }
    }
}