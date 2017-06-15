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

        private IDataProvider _data = new MongoDataProvider();
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
                var course = new Course();
                IDataProvider data = new MongoDataProvider();
                course.ModelCourse = data.Get<DAL.Course>("_id", courseId);

                // Obrati pozornost na to da ne želiš da ti se korisnik može subscribeat 2x
                if (course.ModelCourse.Subscribed.Contains(userMail))
                {
                    return true;
                }
                return DAL.Course.SubscribeUser(userMail, courseId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool UnsubscribeUserFromCourse(string userMail, string courseId)
        {
            try
            {
                var course = new Course();
                IDataProvider data = new MongoDataProvider();
                course.ModelCourse = data.Get<DAL.Course>("_id", courseId);

                // Ne bi trebao biti slučaj, ali ako korisnik nije subscribean onda je sve ok
                if (!course.ModelCourse.Subscribed.Contains(userMail))
                {
                    return true;
                }
                return DAL.Course.UnsubscribeUser(userMail, courseId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string CreateCourse(string name, string description, string author)
        {
            try
            {
                var dalCourse = new DAL.Course();
                dalCourse.Author = author;
                dalCourse.Description = description;
                dalCourse.Lessons = new List<Model.Lesson>();
                dalCourse.Name = name;
                dalCourse.Statistics = new Model.CourseStatistics();
                dalCourse.Subscribed = new List<string>();
                
                return DAL.Course.CreateCourse(dalCourse);
            }
            catch(Exception)
            {
                return null;
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
