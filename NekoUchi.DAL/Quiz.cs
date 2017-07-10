using NekoUchi.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace NekoUchi.DAL
{
    public class Quiz : Model.Quiz, ISerializationHelper
    {
        public ObjectId _id { get; set ; }

        public static Model.QnA GetQnA(string courseId, string lessonName, string username, int orderNo)
        {
            try
            {
                var data = new MongoDataProvider();
                var db = data.GetDatabase();
                var collection = db.GetCollection<Model.QnA>("QnA");

                var wanted = collection.Find(q => q.CourseId == courseId &&
                                                    q.LessonName == lessonName &&
                                                    q.Username == username &&
                                                    q.OrderNo == orderNo).First();
                return wanted;
            }
            catch (Exception)
            {
                return null;
            }           
        }
    }
}
