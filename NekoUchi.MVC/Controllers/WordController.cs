using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NekoUchi.BLL;
using NekoUchi.MVC.Model;

namespace NekoUchi.MVC.Controllers
{
    public class WordController : Controller
    {
        // GET: Word
        public ActionResult Index(string token = "", string filter = "", bool sByMeaning = true, bool ascending = true)
        {
            if (token != "")
            {
                ViewData["token"] = token;
            }            

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

        // GET: Word/Details/5
        public ActionResult Details(string identification, string token = "")
        {
            if (token != "")
            {
                ViewData["token"] = token;
            }            
            return View();
        }

        // GET: Word/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Word/Create
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

        // GET: Word/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Word/Edit/5
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

        // GET: Word/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Word/Delete/5
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