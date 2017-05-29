using NekoUchi.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace NekoUchi.DAL
{
    public class Course : Model.Course, ISerializationHelper
    {
        public ObjectId _id { get; set; }

        public new string Identification { get => _id.ToString(); }
    }
}
