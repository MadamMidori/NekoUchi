using NekoUchi.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace NekoUchi.BLL
{
    public class Course
    {
        #region Properties
        public Model.Course ModelCourse { get; set; }
        public List<Model.Course> ModelCourses { get; set; }

        private IDataProvider data = new MongoDataProvider();
        #endregion

        #region Static methods
        public static Course GetAllCourses()
        {
            try
            {
                IDataProvider data = new MongoDataProvider();
                var course = new Course();
                var enumerableCourses = data.GetMultiple<DAL.Course>("", "");
                course.ModelCourses = new List<Model.Course>();
                foreach (var enumerableCourse in enumerableCourses)
                {
                    enumerableCourse.Identification = enumerableCourse._id.ToString();
                    course.ModelCourses.Add(enumerableCourse);
                }
                return course;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Course GetUsersCourses(string email)
        {
            try
            {
                IDataProvider data = new MongoDataProvider();
                var course = new Course();
                var enumerableCourses = data.GetMultiple<DAL.Course>("Author", email);
                course.ModelCourses = new List<Model.Course>();
                foreach (var enumerableCourse in enumerableCourses)
                {
                    enumerableCourse.Identification = enumerableCourse._id.ToString();
                    course.ModelCourses.Add(enumerableCourse);
                }
                return course;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Course GetSubscribedCourses(string email)
        {
            try
            {
                IDataProvider data = new MongoDataProvider();
                var course = new Course();
                var enumerableCourses = data.GetMultipleUsingAny<DAL.Course>("Subscribed", email);                
                course.ModelCourses = new List<Model.Course>();
                foreach (var enumerableCourse in enumerableCourses)
                {
                    enumerableCourse.Identification = enumerableCourse._id.ToString();
                    course.ModelCourses.Add(enumerableCourse);
                }
                return course;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Course GetCourse(string id)
        {
            try
            {
                var course = new Course();
                IDataProvider data = new MongoDataProvider();
                course.ModelCourse = data.Get<DAL.Course>("_id", id);
                return course;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool SubscribeUserToCourse(string userMail, string courseId)
        {
            try
            {
                IDataProvider data = new MongoDataProvider();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }

    public class Lesson
    {
        public Model.Lesson ModelLesson { get; set; }
    }

    public class CourseStatistics
    {
        public Model.CourseStatistics ModelCourseStatistics { get; set; }
    }
}
