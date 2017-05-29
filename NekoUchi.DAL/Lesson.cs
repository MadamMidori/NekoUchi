using NekoUchi.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace NekoUchi.DAL
{
    public class Lesson : Model.Lesson, ISerializationHelper
    {
        public ObjectId _id { get; set; }
    }
}
