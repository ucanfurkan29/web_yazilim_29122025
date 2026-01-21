using _12_fluentvalidation.Models;
using Microsoft.AspNetCore.Mvc;

namespace _12_fluentvalidation.Controllers
{
    public class OgrenciController : Controller
    {
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Ogrenci model)
        {
            //Fluent validaton arka planda çalıştı ve sonuçları ModelState e ekledi.
            if (!ModelState.IsValid)
            {
                //Hata varsa tekrar formu göster.
                return View(model);
            }
            ViewBag.Mesaj = "Öğrenci başarıyla eklendi.";
            return View();
        }
    }
}
