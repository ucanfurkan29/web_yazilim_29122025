using Microsoft.AspNetCore.Mvc;

namespace _01_program_route.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
