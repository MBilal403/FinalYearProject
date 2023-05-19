using FYP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FYP.Controllers
{
   
    public class AuthController : Controller
    {
        // GET: AuthController
        public ActionResult Index()
        {
            return View("Login");
        }

        // POST: AuthController/Login
        [HttpPost]
        public ActionResult Index(IFormCollection collection)
        {
            string email = collection["LoginEmailInput"];
            email = email.ToLower().Trim();
            string passsword = collection["LoginPasswordInput"];

            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(passsword))
            {
                ViewData["MyMessage"] = "Must be fields filled";
                return View("Login"); // redirect pr view bag or view data does not work
            } else if (!email.Contains("@eduspace.com", stringComparison))
            {
                ViewBag.MyMessage = "Must be used @eduspace.com";
                return View("Login");
            }
            else
            {
                return RedirectToAction("index", "Dashboard");
            }
        }
        // GET: AuthController/Create
        public ActionResult ForgotPassword()
        {
            return View();
        }
        // GET: AuthController/Signup
        public ActionResult Signup()
        {
            return View();
        }
        // GET: AuthController/verification
        public ActionResult Verification()
        {
            return View();
        }

        // POST: AuthController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(IFormCollection collection)
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

        // GET: AuthController/Edit/5
        public ActionResult EditDepartment(int id)
        {

            return View();
        }

        // POST: AuthController/Edit/5
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

        // GET: AuthController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthController/Delete/5
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
        // GET: AuthController/Logout/
        public ActionResult Logout()
        {
            return RedirectToAction("index");
        }

    }
}
