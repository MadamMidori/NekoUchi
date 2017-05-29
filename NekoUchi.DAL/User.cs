using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace NekoUchi.DAL
{
    public class User : Model.User, Helpers.ISerializationHelper
    {
        #region Properties
        public Model.User ModelUser { get; set; }
        public ObjectId _id { get; set; }
        #endregion
    }
}
