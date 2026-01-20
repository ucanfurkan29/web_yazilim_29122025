using Microsoft.AspNetCore.Mvc;
using KampusEtkinlik.Models;

namespace KampusEtkinlik.Controllers
{
    public class EtkinlikController : Controller
    {
        // VERİTABANI SİMÜLASYONU (Static List)
        // Uygulama kapanana kadar veriler burada durur.
        static List<Etkinlik> etkinlikler = new List<Etkinlik>()
        {
            new Etkinlik { 
                Id=1, 
                Baslik="Bahar Şenliği", 
                Aciklama="Müzik ve eğlence", 
                Tarih=DateTime.Now.AddDays(5), 
                ResimUrl="https://placehold.co/300x200/orange/white" 
            },
            new Etkinlik { 
                Id=2, 
                Baslik="Yazılım Semineri", 
                Aciklama="C# ve MVC konuşacağız", 
                Tarih=DateTime.Now.AddDays(2), 
                ResimUrl="https://placehold.co/300x200/blue/white" 
            }
        };

        // KONU 02: Controller to View (Model Gönderme)
        public IActionResult Index()
        {
            ViewBag.SayfaBasligi = "Güncel Etkinlikler"; // KONU 05: ViewBag
            return View(etkinlikler); // Listeyi View'a gönderiyoruz
        }

        // Formu Gösteren Metot
        public IActionResult Ekle()
        {
            return View();
        }

        // Formdan Gelen Veriyi Kaydeden Metot
        [HttpPost]
        public IActionResult Ekle(Etkinlik yeniEtkinlik) // KONU 03: View to Controller
        {
            // Basit bir ID atama işlemi
            yeniEtkinlik.Id = etkinlikler.Count + 1;

            // Listeye ekle
            etkinlikler.Add(yeniEtkinlik);

            // KONU 05: TempData (Yönlendirme sonrası mesaj taşıma)
            TempData["Mesaj"] = "Etkinlik başarıyla eklendi!";

            return RedirectToAction("Index");
        }
    }
}