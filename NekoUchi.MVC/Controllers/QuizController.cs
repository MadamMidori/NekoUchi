using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NekoUchi.BLL;
using Microsoft.Extensions.Primitives;

namespace NekoUchi.MVC.Controllers
{
    public class QuizController : Controller
    {
        public void PrepareQuiz(string token, string courseId, string lessonName, string quizId)
        {
            // save the quiz to db
            var quiz = new Quiz();
            string username = AuthLogic.CheckToken(token);
            if (username == "")
            {
                throw new Exception("NotAuthorized");
            }
            quiz.QnAs = Quiz.GenerateQnA(quizId, courseId, lessonName, username);
            quiz.QnAs = Quiz.Shuffle(quiz.QnAs);

            if (!Quiz.SaveQnAs(quiz.QnAs))
            {
                throw new Exception("Someting went wrong...");
            }

            // call something else to pick it up and start running
            QnA(courseId, lessonName, token, 1);            
        }

        public ActionResult QnA(string courseId, string lessonName, string token, int orderNo)
        {
            // Get the QnA in question from the DB
            string username = AuthLogic.CheckToken(token);
            if (username == "")
            {
                throw new Exception("NotAuthorized");
            }

            var qna = Quiz.GetQnA(courseId, lessonName, username, orderNo);

            // Show it on the screen
            return View(qna);
        }

        [HttpPost]
        public ActionResult QnA(IFormCollection collection)
        {
            var question = new StringValues();
            var courseId = new StringValues();
            var lessonName = new StringValues();
            var token = new StringValues();            
            var orderNoString = new StringValues();
            if (collection.TryGetValue("Question", out question) && 
                    collection.TryGetValue("CourseId", out courseId) &&
                    collection.TryGetValue("LessonName", out lessonName) &&
                    collection.TryGetValue("Token", out token) &&
                    collection.TryGetValue("OrderNo", out orderNoString))
            {
                string username = AuthLogic.CheckToken(token.ToString());
                if (username == "")
                {
                    throw new Exception("NotAuthorized");
                }
                int orderNo = Convert.ToInt32(orderNoString.ToString());
                var qna = Quiz.GetQnA(courseId.ToString(), lessonName, username, orderNo);

                // Evaluate answer
                if (question == qna.Answer)
                {
                    // if the answer is correct, update stats and send the new question
                    // update stats
                    // fix the qna (Done = true)
                    // send in the next thing
                }
                else
                {
                    // if the answer is not correct, ask again (unless Penalty == 3)
                    // update stats
                    // fix the qna (Penalty ++)
                    // if (penalty == 3), get to the next question
                    // else just send the same one
                }
                return RedirectToAction("QnA", new { courseId = courseId.ToString(), lessonName = lessonName.ToString(), token = token.ToString(), orderNo = orderNo });
            }
            else
            {
               return RedirectToAction("Error");
            }
        }

        // GET: Quiz
        public ActionResult Index()
        {
            return View();
        }

        // GET: Quiz/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Quiz/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quiz/Create
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

        // GET: Quiz/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Quiz/Edit/5
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

        // GET: Quiz/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Quiz/Delete/5
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