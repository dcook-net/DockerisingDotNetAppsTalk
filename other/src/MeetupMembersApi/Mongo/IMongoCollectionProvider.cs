using MongoDB.Driver;

namespace MeetupMembers.Mongo
{
    public interface IMongoCollectionProvider<T>
    {
        IMongoCollection<T> GetCollection();
    }
}