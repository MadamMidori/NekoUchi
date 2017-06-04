using NekoUchi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NekoUchi.BLL;

namespace NekoUchi.MVC.Model
{
    public class CourseView
    {
        #region Properties
        public string Identification { get; set; }

        [Display(Name = "Ime")]
        public string CourseName { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Autor")]
        public string Author { get; set; }

        public List<GridLessonView> Lessons { get; set; }
        public List<SubscribersView> Subscribed { get; set; }
        public CourseStatisticsView Statistics { get; set; }
        #endregion

        #region Static methods
        public static CourseView CastFromCourseModel(NekoUchi.Model.Course course)
        {
            var courseView = new CourseView();

            courseView.Author = course.Author;
            courseView.CourseName = course.Name;
            courseView.Description = course.Description;
            courseView.Identification = course.Identification;

            courseView.Lessons = GridLessonView.CastFromLessonModel(course.Lessons);
            courseView.Subscribed = SubscribersView.CastFromSubscribers(course.Subscribed);
            courseView.Statistics = CourseStatisticsView.CastStats(course.Statistics);

            return courseView;
        }
        #endregion
    }

    public class CourseStatisticsView
    {
        #region Properties
        #endregion

        #region Static methods
        public static CourseStatisticsView CastStats(NekoUchi.Model.CourseStatistics statistics)
        {
            var courseStats = new CourseStatisticsView();
            return courseStats;
        }
        #endregion

    }

    public class SubscribersView
    {
        #region Properties
        [Display(Name = "Korisnik")]
        public string Email { get; set; }
        #endregion

        #region Static methods
        public static List<SubscribersView> CastFromSubscribers(List<string> subscribed)
        {
            var subscribersViews = new List<SubscribersView>();

            foreach (var subscriber in subscribed)
            {
                var subscriberView = new SubscribersView();
                subscriberView.Email = subscriber;
                subscribersViews.Add(subscriberView);
            }

            return subscribersViews;
        }
        #endregion
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
        public static List<MyCourseView> CastFromCourse(List<NekoUchi.Model.Course> modelCourses)
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
        public static List<SubscribedCourseView> CastFromCourse(List<NekoUchi.Model.Course> modelCourses)
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
