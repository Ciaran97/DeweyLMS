using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeweyLMS.Controllers
{
    public class ReplacingBooksController : Controller
    {
        // GET: ReplacingBooks
        public ActionResult Index()
        {
            
            return View(GetRandomCallsigns());
        }

        public ActionResult StartGame()
        {
            
            return View(GetRandomCallsigns());
        }

        // GET: ReplacingBooks/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReplacingBooks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReplacingBooks/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: ReplacingBooks/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReplacingBooks/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        // GET: ReplacingBooks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReplacingBooks/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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

        public List<string> GetRandomCallsigns()
        {
            Random _random = new Random();
            List<string> CallNumbers = new List<string>();


            for(int i = 0; i < 10; i++)
            {

                CallNumbers.Add(_random.Next(999).ToString().PadLeft(3, '0') + "." + _random.Next(999).ToString().PadLeft(3, '0'));
              //  System.Diagnostics.Debug.WriteLine(callNumber);

            }




            return CallNumbers;
        }
    }
}
