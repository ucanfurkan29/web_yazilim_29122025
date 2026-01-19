using _06_html_helper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace _06_html_helper.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //User modelinden boþ bir nesne oluþturduk.
            var user = new User();

            //Dropdown seçenekleri dolduruldu
            user.CountryList = GetCountries();

            user.Name = "Furkan";

            return View(user);
        }

        [HttpPost]
        public IActionResult Submit(User user)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = $"Name: {user.Name} - Age: {user.Age} - Gender: {user.Gender} - Country: {user.Country}";
                return View("Result");
            }
            //Hata varsa ayný formu yeniden göster
            //Not: dropdown gibi harici kaynaklý listeler postback iþleminde boþ kalabilir. bu sepeten yeniden doldurmak gerekir.
            user.CountryList = GetCountries();
            return View("Index", user);
        }

        public IActionResult Result()
        {
            return View();
        }

        public IEnumerable<SelectListItem> GetCountries()
        {
            return new SelectListItem[]
            {
                new SelectListItem{Value="US", Text= "USA"},
                new SelectListItem{Value="TR", Text= "Türkiye"},
                new SelectListItem{Value="JP", Text= "Japonya"}
            };
        }
    }
}
