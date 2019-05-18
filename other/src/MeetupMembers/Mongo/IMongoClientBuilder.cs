using MongoDB.Driver;

namespace MeetupMembers.Mongo
{
    public interface IMongoClientBuilder
    {
        IMongoClient Build(MongoConfiguration mongoConfig);
    }
}