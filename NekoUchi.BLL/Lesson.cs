using NekoUchi.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace NekoUchi.BLL
{
    public class Lesson
    {
        #region Properties
        public Model.Lesson ModelLesson { get; set; }
        #endregion

        #region Static methods
        public static bool AddWordToLesson (string courseId, string lessonName, Model.Word word)
        {
            try
            {
                return DAL.Lesson.AddWordToLesson(word, courseId, lessonName);
            }
            catch (Exception)
            {
                return false;
            }            
        }
        #endregion
    }
}
