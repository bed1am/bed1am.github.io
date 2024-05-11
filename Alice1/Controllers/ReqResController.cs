using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alice1.Controllers
{
    public class ReqResController : Controller
    {
        // GET: basic
        public ActionResult Index()
        {
            return View();
        }

        // GET: basic/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: basic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: basic/Create
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

        // GET: basic/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: basic/Edit/5
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

        // GET: basic/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: basic/Delete/5
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
