using NekoUchi.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace NekoUchi.DAL
{
    public class Course : Model.Course, ISerializationHelper
    {
        #region Properties
        public ObjectId _id { get; set; }        
        #endregion

        #region Static methods
        public static bool SubscribeUser(string email, string courseId)
        {
            try
            {
                ObjectId id = new ObjectId(courseId);

                var dataProvider = new MongoDataProvider();
                var db = dataProvider.GetDatabase();
                var collection = db.GetCollection<Course>("Course");

                var filter = Builders<Course>.Filter.Where(x => x._id == id);
                var update = Builders<Course>.Update.AddToSet(x => x.Subscribed, email);
                var result = collection.UpdateOne(filter, update);
                if (result.ModifiedCount == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool AddLesson (Model.Lesson lesson, string courseId)
        {
            try
            {
                ObjectId id = new ObjectId(courseId);

                var dataProvider = new MongoDataProvider();
                var db = dataProvider.GetDatabase();
                var collection = db.GetCollection<Course>("Course");

                var filter = Builders<Course>.Filter.Where(c => c._id == id);
                var update = Builders<Course>.Update.AddToSet(c => c.Lessons, lesson);
                var result = collection.UpdateOne(filter, update);
                if (result.ModifiedCount == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool UpdateLesson (Model.Lesson lesson, string courseId)
        {
            try
            {
                ObjectId id = new ObjectId(courseId);

                var dataProvider = new MongoDataProvider();
                var db = dataProvider.GetDatabase();
                var collection = db.GetCollection<Course>("Course");

                var filter = Builders<Course>.Filter.And(
                                    Builders<Course>.Filter.Where(c => c._id == id),
                                    Builders<Course>.Filter.ElemMatch(c => c.Lessons, l => l.Name == lesson.Name));
                var update = Builders<Course>.Update.Set(c => c.Lessons[-1], lesson);
                var result = collection.UpdateOne(filter, update);
                if (result.ModifiedCount == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool UnsubscribeUser(string email, string courseId)
        {
            try
            {
                ObjectId id = new ObjectId(courseId);

                var dataProvider = new MongoDataProvider();
                var db = dataProvider.GetDatabase();
                var collection = db.GetCollection<Course>("Course");

                var filter = Builders<Course>.Filter.Where(x => x._id == id);
                var update = Builders<Course>.Update.Pull(x => x.Subscribed, email);
                var result = collection.UpdateOne(filter, update);
                if (result.ModifiedCount == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string CreateCourse(Course newCourse)
        {
            IDataProvider data = new MongoDataProvider();
            newCourse = data.Create(newCourse);
            return newCourse._id.ToString();
        }
        #endregion
    }
}
