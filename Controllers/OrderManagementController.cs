using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Zamowienia.Controllers
{
    public class OrderManagementController : Controller
    {
        // GET: OrderManagementController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderManagementController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderManagementController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderManagementController/Create
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

        // GET: OrderManagementController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderManagementController/Edit/5
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

        // GET: OrderManagementController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderManagementController/Delete/5
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
