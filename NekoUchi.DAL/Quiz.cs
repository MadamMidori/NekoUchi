using NekoUchi.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace NekoUchi.DAL
{
    public class Quiz : Model.Quiz, ISerializationHelper
    {
        public ObjectId _id { get; set ; }
    }
}
