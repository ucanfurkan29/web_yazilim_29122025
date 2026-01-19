using _07_custom_helpers.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _07_custom_helpers.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            var formatted = StringHelper.CapitalizeFirstLetter("databaseden gelen veri");
            ViewBag.Message = formatted;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
