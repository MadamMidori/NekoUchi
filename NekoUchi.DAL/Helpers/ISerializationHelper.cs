using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace NekoUchi.DAL.Helpers
{
    public interface ISerializationHelper
    {
        ObjectId _id { get; set; }
    }
}
