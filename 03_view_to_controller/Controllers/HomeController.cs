using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _03_view_to_controller.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string ad, string secenekler, bool onay = false)
        {
            var k1 = Request.Form["secenekler"];
            var a1 = Request.Form["ad"];
            var o1 = Request.Form["onay"];

            ViewBag.name = k1;
            return View();
        }

    }
}

/*
 Ýstenenler (View Tarafý): Bir Index view'ý içerisinde (Ýster HTML Helper, ister standart HTML etiketi kullanarak) þu 3 bilgiyi alan bir form oluþturun:
        Öðrenci Adý Soyadý: (Metin kutusu)
        Ýlgilendiði Alan: (Açýlýr Liste / Dropdown - Seçenekler: Web Geliþtirme, Oyun Geliþtirme, Yapay Zeka)
        Hazýrlýk Sýnýfý mý?: (Checkbox / Ýþaret kutusu)
        "Baþvur" butonu.

        Ýstenenler (Controller Tarafý):
        Form post edildiðinde çalýþan Index metodunu yazýn.
        Gelen verileri parametre olarak yakalayýn.
        ViewBag kullanarak ekrana þu formatta bir mesaj yazdýrýn:
        "Sayýn [Ad Soyad], [Alan] alanýndaki baþvurunuz alýnmýþtýr. Hazýrlýk sýnýfý durumu: [True/False]"
 */