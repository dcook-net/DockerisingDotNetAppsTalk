using MongoDB.Driver;

namespace MeetupMembersApi.Mongo
{
    public interface IMongoClientBuilder
    {
        IMongoClient Build(MongoConfiguration mongoConfig);
    }
}