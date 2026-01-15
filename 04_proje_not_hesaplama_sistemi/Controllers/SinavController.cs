using Microsoft.AspNetCore.Mvc;

namespace _04_proje_not_hesaplama_sistemi.Controllers
{
    public class SinavController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string adSoyad, double vize, double final)
        {
            double ortalama = (vize * 0.4) + (final * 0.6);

            string durum = ortalama >= 50 ? "GEÇTİ" : "KALDI";
            string renk = ortalama >= 50 ? "green" : "red";

            //İki şekilde de yapılabilir.
            //if (ortalama >= 50)
            //{
            //    durum = "GEÇTİ";
            //    renk = "green";
            //}
            //else
            //{
            //    durum = "KALDI";
            //    renk = "red";
            //}

            ViewBag.OgrenciAdi = adSoyad;
            ViewBag.Ortalama = ortalama;
            ViewBag.Durum = durum;
            ViewBag.Renk = renk;
            ViewBag.SonucGoster = true;

            return View();
        }

    }
}
