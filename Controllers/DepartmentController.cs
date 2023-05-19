using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FYP.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: DeaprtmentController
       
        public ActionResult ViewDepartments()
        {

            return View();
        }

        // GET: DeaprtmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DeaprtmentController/Create
        public ActionResult AddDepartment()
        {
            return View();
        }

        // POST: DeaprtmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(ViewDepartments));
            }
            catch
            {
                return View();
            }
        }

        // GET: DeaprtmentController/Edit/5
        public ActionResult EditDepartment(int id)
        {
            return View();
        }

        // POST: DeaprtmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepartment(int id, IFormCollection collection)
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

        // GET: DepartmentController/Delete/5
        public ActionResult DeleteDepartment(int id)
        {

            return View();
        }

        // POST: DeaprtmentController/Delete/5
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
