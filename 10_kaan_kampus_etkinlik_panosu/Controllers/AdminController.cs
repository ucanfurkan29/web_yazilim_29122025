using _10_kaan_kampus_etkinlik_panosu.Models;
using Microsoft.AspNetCore.Mvc;

namespace _10_kaan_kampus_etkinlik_panosu.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Ekle()
        {
            ViewBag.Title = "Etkinlik Ekle";
            return View(new Etkinlik { Tarih = DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Ekle(Etkinlik model)
        {
            ViewBag.Title = "Etkinlik Ekle";

            if (!ModelState.IsValid)
                return View(model);

            model.Id = HomeController.NextId();
            HomeController.Etkinlikler.Add(model);

            TempData["Success"] = "Etkinlik Başarıyla Eklendi!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ListeyiTemizle()
        {
            HomeController.ClearAll();
            TempData["Success"] = "Etkinlik listesi temizlendi.";
            return RedirectToAction("Index", "Home");
        }
    }
}
