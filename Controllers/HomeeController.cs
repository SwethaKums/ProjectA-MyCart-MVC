using Microsoft.AspNetCore.Mvc;

namespace MyCartMVC.Controllers
{
    public class HomeeController : Controller
    {
        public IActionResult Indexes()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


    }
}
