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

        #endregion

        #region StaticMethods
        #endregion
    }

    public class GridLessonView
    {
        #region Properties
        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        public string Identification { get; set; }
        #endregion

        #region Static methods
        public static List<GridLessonView> CastFromLessonModel(List<Lesson> lessons)
        {
            var lessonViews = new List<GridLessonView>();

            foreach (var lesson in lessons)
            {
                var lessonView = new GridLessonView();
                lessonView.Description = lesson.Description;
                lessonView.Name = lesson.Name;
                lessonViews.Add(lessonView);
            }

            return lessonViews;
        }
        #endregion
    }
}
