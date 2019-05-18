using MongoDB.Driver;

namespace MeetupMembers.Mongo
{
    public interface IMongoDatabaseProvider
    {
        IMongoDatabase GetDatabase();
        string MongoUrl { get; } 
    }
}