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
    public class ProfileController : Controller
    {      
        // GET: Profile
        public ActionResult Index(string token)
        {            
            var profile = new ProfileView();

            // Validate token and get user from DB
            var user = BLL.User.Get(AuthLogic.CheckToken(token));
            ViewData["token"] = token;

            // Get users courses and stats from DB
            // owned courses
            var ownedCourses = Course.GetUsersCourses(user.ModelUser.Email);
            if (ownedCourses != null)
            {
                profile.MyCourses = MyCourseView.CastFromCourse(ownedCourses.ModelCourses);
            }
            else
            {
                profile.MyCourses = new List<MyCourseView>();
            }

            // subscribed courses
            var subscribedCourses = Course.GetSubscribedCourses(user.ModelUser.Email);
            if (subscribedCourses != null)
            {
                profile.SubscribedCourses = SubscribedCourseView.CastFromCourse(subscribedCourses.ModelCourses);
            }
            else
            {
                profile.SubscribedCourses = new List<SubscribedCourseView>();
            }
            
            // stats

            // Show 'em
            profile.Email = user.ModelUser.Email;
            profile.RegistrationDate = user.ModelUser.RegistrationDate;
            return View(profile);
        }

        // GET: Profile/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
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

        // GET: Profile/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Profile/Edit/5
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

        // GET: Profile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profile/Delete/5
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