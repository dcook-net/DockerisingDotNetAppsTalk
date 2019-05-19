using MongoDB.Driver;

namespace MeetupMembersApi.Mongo
{
    public interface IMongoDatabaseProvider
    {
        IMongoDatabase GetDatabase();
        string MongoUrl { get; } 
    }
}