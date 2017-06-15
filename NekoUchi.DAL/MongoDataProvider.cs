using MongoDB.Bson;
using MongoDB.Driver;
using NekoUchi.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NekoUchi.DAL
{
    public class MongoDataProvider : IDataProvider
    {
        private IMongoDatabase _db;

        public MongoDataProvider(bool isLocal = true)
        {
            if (isLocal)
            {
                _db = GetLocalDatabase("NekoUchiDev");
            }
            else
            {
                _db = GetRemoteDatabase();
            }
        }       

        private IMongoDatabase GetLocalDatabase(string dbName)
        {
            var settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress("localhost", 27017);
            IMongoClient _client = new MongoClient(settings);
            return _client.GetDatabase(dbName);
        }

        private IMongoDatabase GetRemoteDatabase()
        {            
            var settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(Constants.RemoteServer, Constants.RemoteIP);
            var credential = MongoCredential.CreateCredential(Constants.RemoteDB, Constants.username, Constants.password);
            settings.Credentials = new[] { credential };

            IMongoClient _client = new MongoClient(settings);
            return _client.GetDatabase("nekouchidev");
        }

        public IMongoDatabase GetDatabase()
        {
            return _db;
        }

        public T Get<T>(string field, string value)
        {
            try
            {
                var collection = _db.GetCollection<T>(typeof(T).Name);
                FilterDefinition<T> filter = Builders<T>.Filter.Eq(field, value);
                if (field == "_id")
                {
                    ObjectId objId = new ObjectId(value);
                    filter = Builders<T>.Filter.Eq(field, objId);
                }
                else
                {
                    filter = Builders<T>.Filter.Eq(field, value);
                }
                var result = collection.Find(filter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public IEnumerable<T> GetMultiple<T>(string field, string value)
        {
            try
            {
                var collection = _db.GetCollection<T>(typeof(T).Name);
                FilterDefinition<T> filter = null;
                if (field == "")
                {
                    filter = Builders<T>.Filter.Empty;
                }
                else
                {
                    filter = Builders<T>.Filter.Eq(field, value);
                }
                var result = collection.Find(filter).ToList();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<T> GetMultipleUsingAny<T>(string field, string value)
        {
            try
            {
                var collection = _db.GetCollection<T>(typeof(T).Name);
                FilterDefinition<T> filter = Builders<T>.Filter.AnyEq(field, value);
                var result = collection.Find(filter).ToList();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public T Create<T>(T item)
        {
            try
            {
                var collection = _db.GetCollection<T>(typeof(T).Name);
                collection.InsertOne(item);
                return item;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public bool Update<T>(Dictionary<string, string> changes, string field, string value)
        {
            try
            {
                var collection = _db.GetCollection<T>(typeof(T).Name);
                UpdateDefinition<T> update = null;
                foreach (var fieldToChange in changes.Keys)
                {
                    update = Builders<T>.Update.Set(fieldToChange, changes[fieldToChange]);
                }
                FilterDefinition<T> filter = Builders<T>.Filter.Eq(field, value);
                return collection.UpdateOne(filter, update).IsAcknowledged;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete<T>(string field, string value)
        {
            try
            {
                var collection = _db.GetCollection<T>(typeof(T).Name);
                FilterDefinition<T> filter = Builders<T>.Filter.Eq(field, value);
                return collection.DeleteOne(filter).IsAcknowledged;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
