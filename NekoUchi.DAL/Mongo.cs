using MongoDB.Driver;
using System;

namespace NekoUchi.DAL
{
    public class Mongo
    {
        public static IMongoDatabase GetLocalDatabase(string dbName)
        {
            var settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress("localhost", 27017);
            IMongoClient _client = new MongoClient(settings);
            return _client.GetDatabase(dbName);
        }
    }
}
