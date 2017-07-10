using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NekoUchi.BLL;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;
using NekoUchi.MVC.Model;

namespace NekoUchi.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult Register()
        {            
            return View(new RegisterView());
        }

        [HttpPost]
        public IActionResult Register(IFormCollection collection)
        {
            var email = new StringValues();
            var pass = new StringValues();
            if (collection.TryGetValue("Email", out email) && collection.TryGetValue("Password", out pass))
            {
                try
                {
                    var auth = new AuthLogic();
                    auth.Email = email.ToString();
                    auth.Password = pass.ToString();
                    string token = auth.Register();
                    if (token == "User exists")
                    {
                        return View("UserExists");
                    }
                    return RedirectToAction("Index", "Profile", new { token = token });
                }
                catch (Exception)
                {
                    return Error();
                }
            }
            else
            {
                return Error();
            }
        }

        public IActionResult Login()
        {
            return View(new LoginView());
        }

        [HttpPost]
        public IActionResult Login(IFormCollection collection)
        {
            var email = new StringValues();
            var pass = new StringValues();
            string token = "";
            if (collection.TryGetValue("Email", out email) && collection.TryGetValue("Password", out pass))
            {
                try
                {
                    var auth = new AuthLogic();
                    auth.Email = email.ToString();
                    auth.Password = pass.ToString();
                    token = auth.Login();
                    return RedirectToAction("Index", "Profile", new { token = token });
                }
                catch (Exception)
                {
                    return Error();
                }
            }
            else
            {
                return Error();
            }

        }

        public IActionResult Logout(string token)
        {
            if(AuthLogic.DeleteToken(token))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Error();
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
