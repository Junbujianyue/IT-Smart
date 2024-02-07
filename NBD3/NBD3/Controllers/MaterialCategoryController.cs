using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NBD3.Controllers
{
    public class MaterialCategoryController : Controller
    {
        // GET: MaterialCategoryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MaterialCategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MaterialCategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaterialCategoryController/Create
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

        // GET: MaterialCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MaterialCategoryController/Edit/5
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

        // GET: MaterialCategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MaterialCategoryController/Delete/5
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
