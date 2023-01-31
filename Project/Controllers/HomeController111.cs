using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop_Project.Controllers
    {
    public class HomeController111 : Controller
        {
        // GET: HomeController111
        public ActionResult Index()
            {
            return View();
            }

        // GET: HomeController111/Details/5
        public ActionResult Details(int id)
            {
            return View();
            }

        // GET: HomeController111/Create
        public ActionResult Create()
            {
            return View();
            }

        // POST: HomeController111/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
            {
            try
                {
                return RedirectToAction(nameof(Index));
                }
            catch
                {
                return View();
                }
            }

        // GET: HomeController111/Edit/5
        public ActionResult Edit(int id)
            {
            return View();
            }

        // POST: HomeController111/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
            {
            try
                {
                return RedirectToAction(nameof(Index));
                }
            catch
                {
                return View();
                }
            }

        // GET: HomeController111/Delete/5
        public ActionResult Delete(int id)
            {
            return View();
            }

        // POST: HomeController111/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
            {
            try
                {
                return RedirectToAction(nameof(Index));
                }
            catch
                {
                return View();
                }
            }
        }
    }
