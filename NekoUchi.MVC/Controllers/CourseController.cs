using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NekoUchi.MVC.Model;
using NekoUchi.BLL;

namespace NekoUchi.MVC.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index(string token)
        { 
            if (AuthLogic.CheckToken(token) == "")
            {
                throw new Exception("NotAuthorized");
            }
            ViewData["token"] = token;
            var allCourses = Course.GetAllCourses();
            var courses = new List<SubscribedCourseView>();
            courses = SubscribedCourseView.CastFromCourse(allCourses.ModelCourses);
            return View(courses);
        }

        // GET: Course/Details/5
        public ActionResult Details(string token, string id)
        {
            if (AuthLogic.CheckToken(token) == "")
            {
                throw new Exception("NotAuthorized");
            }
            ViewData["token"] = token;
            var course = Course.GetCourse(id);

            // cast to Subscribed
            CourseView courseView = CourseView.CastFromCourseModel(course.ModelCourse);

            // return the subscribed one!
            return View(courseView);
        }

        [HttpPost]
        public void Subscribe(string identification)
        {
            string[] identificationParts = identification.Split('+');
            string id = identificationParts[0];
            string token = identificationParts[1];
            string username = AuthLogic.CheckToken(token);
            if (username == "")
            {
                throw new Exception("NotAuthorized");
            }
            ViewData["token"] = token;

            // subscribe the user to the course
            
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
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