using MongoDB.Bson.Serialization.Attributes;

namespace MeetupMembersApi.Mongo
{
    public interface IHaveAUniqueId
    {
        [BsonId]
        string Id { get; }
    }
}