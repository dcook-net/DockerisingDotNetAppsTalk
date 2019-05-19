using MongoDB.Driver;

namespace MeetupMembersApi.Mongo
{
    public class MongoConfiguration
    {
        public string MongoConnection { get; set; } = "mongodb://localhost:27017";
        public int? Timeout { get; set; } = 3000;
        public MongoUrl MongoUrl => new MongoUrl(MongoConnection);
        public string DatabaseName { get; } = "MeetupMembersApi";
        public string CollectionName { get; } = "Members";
    }
}