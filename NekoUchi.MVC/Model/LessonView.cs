using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NekoUchi.Model;

namespace NekoUchi.MVC.Model
{
    public class LessonView
    {
        #region Properties
        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        public string CourseIdentification { get; set; }

        public List<WordView> Words { get; set; }

        public List<QuizView> Quiz { get; set; }

        #endregion

        #region StaticMethods

        public static LessonView CastFromLesson (Lesson lesson, string courseId)
        {
            var view = new LessonView();
            view.Description = lesson.Description;
            view.Name = lesson.Name;
            view.Words = WordView.CastFromModelWord(lesson.Words);
            view.CourseIdentification = courseId;
            return view;
        }

        #endregion
    }

    public class GridLessonView
    {
        #region Properties
        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        public string CourseIdentification { get; set; }
        #endregion

        #region Static methods
        public static List<GridLessonView> CastFromLessonModel(List<Lesson> lessons, string courseId)
        {
            var lessonViews = new List<GridLessonView>();

            foreach (var lesson in lessons)
            {
                var lessonView = new GridLessonView();
                lessonView.Description = lesson.Description;
                lessonView.Name = lesson.Name;
                lessonView.CourseIdentification = courseId;
                lessonViews.Add(lessonView);
            }

            return lessonViews;
        }
        #endregion
    }
}
