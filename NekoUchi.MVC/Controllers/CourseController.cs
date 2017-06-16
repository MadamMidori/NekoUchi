using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NekoUchi.MVC.Model;
using NekoUchi.BLL;
using Microsoft.Extensions.Primitives;

namespace NekoUchi.MVC.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index(string token, string filter = "", bool sByName = true, bool ascending = true)
        { 
            if (AuthLogic.CheckToken(token) == "")
            {
                throw new Exception("NotAuthorized");
            }
            ViewData["token"] = token;

            // Za prvu ruku sve ide iz baze, poslije æu uzimati samo one koji odgovaraju
            // zadanom filtru
            var allCourses = Course.GetAllCourses();

            // filter
            ViewData["filter"] = filter;
            if (filter != null && filter != "")
            {
                allCourses.ModelCourses = allCourses.ModelCourses.Where(c => c.Name.Contains(filter) || c.Author.Contains(filter)).ToList();                
            }

            // sort
            ViewData["ascending"] = ascending;
            if (sByName)
            {
                if (ascending)
                {
                    allCourses.ModelCourses = allCourses.ModelCourses.OrderBy(c => c.Name).ToList();
                }
                else
                {
                    allCourses.ModelCourses = allCourses.ModelCourses.OrderByDescending(c => c.Name).ToList();
                }
            }
            else
            {
                if (ascending)
                {
                    allCourses.ModelCourses = allCourses.ModelCourses.OrderBy(c => c.Author).ToList();
                }
                else
                {
                    allCourses.ModelCourses = allCourses.ModelCourses.OrderByDescending(c => c.Author).ToList();
                }
            }
            
            // paging

            var courses = new List<SubscribedCourseView>();
            courses = SubscribedCourseView.CastFromCourse(allCourses.ModelCourses);
            return View(courses);
        }

        // GET: Course/Details/5
        public ActionResult Details(string token, string id)
        {
            string username = AuthLogic.CheckToken(token);
            if (username == "")
            {
                throw new Exception("NotAuthorized");
            }
            ViewData["token"] = token;
            var course = Course.GetCourse(id);

            // cast to Subscribed
            CourseView courseView = CourseView.CastFromCourseModel(course.ModelCourse);
            if (courseView.Author == username)
            {
                ViewData["isAuthor"] = true;
            }
            else
            {
                ViewData["isAuthor"] = false;
            }
            ViewData["courseId"] = id;

            // return the subscribed one!
            return View(courseView);
        }

        public void Subscribe(string identification)
        {
            string[] identificationParts = identification.Split(' ');
            string id = identificationParts[0];
            string token = identificationParts[1];
            string username = AuthLogic.CheckToken(token);
            if (username == "")
            {
                throw new Exception("NotAuthorized");
            }
            ViewData["token"] = token;

            // subscribe the user to the course
            if (!Course.SubscribeUserToCourse(username, id))
            {
                throw new Exception();
            }           
        }

        public ActionResult Unsubscribe(string courseId, string token)
        {
            string username = AuthLogic.CheckToken(token);
            if (username == "")
            {
                throw new Exception("NotAuthorized");
            }
            ViewData["token"] = token;

            // unsubscribe the user from the course
            if (!Course.UnsubscribeUserFromCourse(username, courseId))
            {
                throw new Exception();
            }

            return RedirectToAction("Index", "Profile", new { token = token });
        }

        // GET: Course/Create
        public ActionResult Create(string token)
        {
            if (AuthLogic.CheckToken(token) == "")
            {
                throw new Exception("NotAutorized");
            }
            ViewData["token"] = token;

            var newCourse = new NewCourseView();
            return View(newCourse);
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var courseName = new StringValues();
                var description = new StringValues();
                var token = new StringValues();
                string id = "";
                if (collection.TryGetValue("CourseName", out courseName) && collection.TryGetValue("Description", out description) && collection.TryGetValue("token", out token))
                {
                    var username = AuthLogic.CheckToken(token.ToString());
                    if (username == "")
                    {
                        throw new Exception("NotAuthorized");
                    }

                    // save course
                    id = Course.CreateCourse(courseName.ToString(), description.ToString(), username);
                }
                return RedirectToAction("Details", new { token = token, id = id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Course/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Course/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}