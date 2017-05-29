using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace NekoUchi.DAL
{
    public class Session
    {
        private ObjectId _id;
        public string Id { get; set; }
        public string Email { get; set; }
        public DateTime TTE { get; set; }
        public string Role { get; set; }

        public bool Create()
        {
            try
            {
                var db = MongoDataProvider.GetLocalDatabase("NekoUchiDev");
                var collection = db.GetCollection<Session>("Session");
                _id = ObjectId.GenerateNewId();             
                Id = _id.ToString();
                collection.InsertOne(this);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }       
    }
}
