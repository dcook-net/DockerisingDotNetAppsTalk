using MongoDB.Driver;

namespace MeetupMembersApi.Mongo
{
    public interface IMongoCollectionProvider<T>
    {
        IMongoCollection<T> GetCollection();
    }
}