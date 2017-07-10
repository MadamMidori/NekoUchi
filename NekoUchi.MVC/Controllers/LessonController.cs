using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NekoUchi.BLL;
using NekoUchi.MVC.Model;
using Microsoft.Extensions.Primitives;

namespace NekoUchi.MVC.Controllers
{
    public class LessonController : Controller
    {
        // GET: Lesson
        public ActionResult Index()
        {
            return View();
        }

        // GET: Lesson/Details/5
        public ActionResult Details(string token, string courseId, string lessonName)
        {
            string username = AuthLogic.CheckToken(token);
            if (username == "")
            {
                throw new Exception("NotAuthorized");
            }
            ViewData["token"] = token;

            var course = Course.GetCourse(courseId);
            if (username == course.ModelCourse.Author)
            {
                ViewData["isAuthor"] = true;
            }
            else
            {
                ViewData["isAuthor"] = false;
            }

            var lessonView = new LessonView();
            foreach(var courseLesson in course.ModelCourse.Lessons)
            {
                if (courseLesson.Name == lessonName)
                {
                    lessonView = LessonView.CastFromLesson(courseLesson, courseId);
                    break;
                }
            }
            if (lessonView.Words == null)
            {
                lessonView.Words = new List<WordView>();
            }

            // For now there are only generic tests; later there might be lesson-specific tests,
            // so those will be included here as well...)
            var bllQuizzes = Quiz.GetAllGenericQuizzes();
            lessonView.Quiz = QuizView.CastFromBllQuiz(bllQuizzes);

            return View(lessonView);
        }

        // GET: Lesson/Create
        public ActionResult Create(string courseId, string token)
        {
            if (AuthLogic.CheckToken(token) == "")
            {
                throw new Exception("NotAuthorized");
            }
            ViewData["token"] = token;

            var lessonView = new GridLessonView();
            lessonView.CourseIdentification = courseId;
            return View(lessonView);
        }

        // POST: Lesson/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var name = new StringValues();
                var description = new StringValues();
                var courseIdentification = new StringValues();
                var token = new StringValues();
                if (collection.TryGetValue("Name", out name) 
                        && collection.TryGetValue("Description", out description) 
                        && collection.TryGetValue("token", out token) 
                        && collection.TryGetValue("CourseIdentification", out courseIdentification))
                {
                    var username = AuthLogic.CheckToken(token.ToString());
                    if (username == "")
                    {
                        throw new Exception("NotAuthorized");
                    }
                    ViewData["token"] = token;

                    // Add lesson to Course
                    var lesson = new Lesson();
                    lesson.ModelLesson = new NekoUchi.Model.Lesson();
                    lesson.ModelLesson.Name = name.ToString();
                    lesson.ModelLesson.Description = description.ToString();
                    lesson.ModelLesson.Words = new List<NekoUchi.Model.Word>();

                    if (Course.AddLessonToCourse(courseIdentification.ToString(), lesson.ModelLesson))
                    {
                        return RedirectToAction("Details", new { token = token.ToString(), courseId = courseIdentification.ToString(), lessonName = name.ToString() });
                    }
                }
                throw new Exception("Something happened...");
            }
            catch
            {
                throw new Exception("Something happened...");
            }
        }

        // GET: Lesson/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Lesson/Edit/5
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

        // GET: Lesson/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Lesson/Delete/5
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

        public ActionResult WordsForLesson(string courseId, string lessonName, string token, string filter = "", bool sByMeaning = true, bool ascending = true)
        {            
            if (AuthLogic.CheckToken(token) == "")
            {
                throw new Exception("NotAuthorized");
            }
            ViewData["token"] = token;
            ViewData["courseId"] = courseId;
            ViewData["lessonName"] = lessonName;

            // Za prvu ruku sve ide iz baze, poslije æu uzimati samo one koji odgovaraju
            // zadanom filtru
            var allWords = Word.GetAllWords();

            // filter          
            ViewData["filter"] = filter;
            if (filter != null && filter != "")
            {
                allWords = allWords.Where(w => w.ModelWord.Meaning.ToLower().Contains(filter.ToLower())
                                                                    || w.ModelWord.Kana.ToLower().Contains(filter.ToLower())
                                                                    || w.ModelWord.Kanji.ToLower().Contains(filter.ToLower())
                                                                    || w.ModelWord.Level.ToLower().Contains(filter.ToLower())).ToList();
            }

            // sort
            ViewData["ascending"] = ascending;
            if (sByMeaning)
            {
                if (ascending)
                {
                    allWords = allWords.OrderBy(w => w.ModelWord.Meaning).ToList();
                }
                else
                {
                    allWords = allWords.OrderByDescending(w => w.ModelWord.Meaning).ToList();
                }
            }
            else
            {
                if (ascending)
                {
                    allWords = allWords.OrderBy(w => w.ModelWord.Level).ToList();
                }
                else
                {
                    allWords = allWords.OrderByDescending(w => w.ModelWord.Level).ToList();
                }
            }

            // paging

            var words = new List<WordView>();
            words = WordView.CastFromBllWord(allWords);
            return View(words);
        }

        public void AddWordToLesson(string wordId, string token, string courseId, string lessonName)
        {
            if (AuthLogic.CheckToken(token) == "")
            {
                throw new Exception("NotAuthorized");
            }

            Word word = Word.GetWord(wordId);
            if(!Lesson.AddWordToLesson(courseId, lessonName, word.ModelWord))
            {
                throw new Exception("Something went wrong...");
            }
        }
    }
}