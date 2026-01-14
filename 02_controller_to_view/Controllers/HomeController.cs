using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _02_controller_to_view.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var products = new List<string>
            {
                "Laptop",
                "Smartphone",
                "Tablet",
                "Smartwatch",
                "Headphones"
            };

            ViewData["Products"] = products;
            //viewData NEDÝR?
            // ViewData, controller'dan view'a veri taþýmak için kullanýlan bir sözlüktür (dictionary).
            // Sadece mevcut HTTP isteði için veri taþýr ve view render edildikten sonra silinir.
            // Tip güvenli (Deðiþken tipi kontrol etmez) deðildir, bu yüzden dikkatli kullanýlmalýdýr.
            return View();
        }

        public IActionResult Details(int id)
        {
            var product = $"{id} Numaralý ürünün detaylarý:";

            ViewData["ProductDetails"] = product;
            return View();
        }
    }
}
