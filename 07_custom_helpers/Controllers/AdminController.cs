using Microsoft.AspNetCore.Mvc;

namespace _07_custom_helpers.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
