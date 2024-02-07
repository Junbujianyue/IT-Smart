using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NBD3.Controllers
{
    public class BidsController : Controller
    {
        // GET: BidsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BidsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BidsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BidsController/Create
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

        // GET: BidsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BidsController/Edit/5
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

        // GET: BidsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BidsController/Delete/5
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
