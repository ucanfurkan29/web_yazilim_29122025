using Microsoft.AspNetCore.Mvc;

namespace _09_render_section_nested.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
