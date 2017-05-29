using NekoUchi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NekoUchi.MVC.Model
{
    public class CourseView
    {

    }

    public class MyCourseView
    {
        #region Properties
        [Display(Name = "Ime")]
        public string CourseName { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Broj pretplaćenih na tečaj")]
        public int NumberOfSubscribers { get; set; }

        public string Identification { get; set; }
        #endregion

        #region Static methods
        public static List<MyCourseView> CastFromCourse(List<Course> modelCourses)
        {
            var myCourses = new List<MyCourseView>();
            foreach (var course in modelCourses)
            {
                var myCourse = new MyCourseView();
                myCourse.CourseName = course.Name;
                myCourse.NumberOfSubscribers = course.Subscribed.Count;
                myCourse.Description = course.Description;
                myCourse.Identification = course.Identification;
                myCourses.Add(myCourse);
            }
            return myCourses;
        }
        #endregion
    }

    public class SubscribedCourseView
    {
        #region Properties
        [Display(Name = "Tečaj")]
        public string CourseName { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Autor")]
        public string CourseAuthor { get; set; }

        public string Identification { get; set; }
        #endregion

        #region Static methods
        public static List<SubscribedCourseView> CastFromCourse(List<Course> modelCourses)
        {
            var subscribedCourses = new List<SubscribedCourseView>();
            foreach (var course in modelCourses)
            {
                var subscribedCourse = new SubscribedCourseView();
                subscribedCourse.CourseAuthor = course.Author;
                subscribedCourse.CourseName = course.Name;
                subscribedCourse.Description = course.Description;
                subscribedCourse.Identification = course.Identification;
                subscribedCourses.Add(subscribedCourse);
            }
            return subscribedCourses;
        }
        #endregion
    }
}
