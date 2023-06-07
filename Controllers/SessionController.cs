using Microsoft.AspNetCore.Mvc;

namespace FYP.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult AddSession()
        {
            return View();
        }
    }
}
