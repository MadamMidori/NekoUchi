using NekoUchi.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace NekoUchi.DAL
{
    public class Lesson : Model.Lesson, ISerializationHelper
    {
        #region Properties
        public ObjectId _id { get; set; }
        #endregion

        #region Static methods
        public static bool AddWordToLesson (Model.Word word, string courseId, string lessonName)
        {
            try
            {
                ObjectId id = new ObjectId(courseId);

                var modelWord = Word.RemoveId(word);

                var dataProvider = new MongoDataProvider();
                var db = dataProvider.GetDatabase();
                var collection = db.GetCollection<Course>("Course");

                var filter = Builders<Course>.Filter.And(
                                    Builders<Course>.Filter.Where(c => c._id == id),
                                    Builders<Course>.Filter.ElemMatch(c => c.Lessons, l => l.Name == lessonName));
                var update = Builders<Course>.Update.AddToSet(c => c.Lessons[-1].Words, modelWord);
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
        #endregion
    }
}
