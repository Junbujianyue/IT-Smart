using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NBD3.Controllers
{
    public class labourController : Controller
    {
        // GET: labBourController
        public ActionResult Index()
        {
            return View();
        }

        // GET: labBourController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: labBourController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: labBourController/Create
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

        // GET: labBourController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: labBourController/Edit/5
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

        // GET: labBourController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: labBourController/Delete/5
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

        public ActionResult labourType(int id)
        {
            return View();
        }
    }
}
