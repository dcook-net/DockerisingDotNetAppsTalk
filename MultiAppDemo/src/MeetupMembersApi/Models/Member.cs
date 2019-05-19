using MeetupMembersApi.Mongo;
using MongoDB.Bson.Serialization.Attributes;

namespace MeetupMembersApi.Models
{
    public class Member : IHaveAUniqueId
    {
        [BsonId]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set;}
    }
}