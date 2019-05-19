using System;
using MongoDB.Driver;

namespace MeetupMembersApi.Mongo
{
    public class MongoClientBuilder : IMongoClientBuilder
    {
        private readonly TimeSpan _defaultTimeout = TimeSpan.FromMilliseconds(5000);

        public IMongoClient Build(MongoConfiguration mongoConfig)
        {
            var mongoTimeout = DetermineTimeout(mongoConfig);

            var settings = MongoClientSettings.FromUrl(mongoConfig.MongoUrl);
            settings.ConnectTimeout = mongoTimeout;
            settings.SocketTimeout = mongoTimeout;
            settings.ServerSelectionTimeout = mongoTimeout;

            return new MongoClient(settings);
        }

        private TimeSpan DetermineTimeout(MongoConfiguration configuration)
        {
            return configuration.Timeout == null ? _defaultTimeout : TimeSpan.FromMilliseconds(configuration.Timeout.Value);
        }
    }
}