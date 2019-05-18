using MongoDB.Bson.Serialization.Attributes;

namespace MeetupMembers.Mongo
{
    public interface IHaveAUniqueId
    {
        [BsonId]
        string Id { get; }
    }
}