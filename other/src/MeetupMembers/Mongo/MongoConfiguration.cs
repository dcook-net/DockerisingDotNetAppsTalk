using MongoDB.Driver;

namespace MeetupMembers.Mongo
{
    public class MongoConfiguration
    {
        public string MongoConnection { get; set; }// = "mongodb://localhost:27017";
        public int? Timeout { get; set; } = 3000;
        public MongoUrl MongoUrl => new MongoUrl(MongoConnection);
        public string DatabaseName { get; } = "MeetupMembers";
        public string CollectionName { get; } = "Members";
    }
}