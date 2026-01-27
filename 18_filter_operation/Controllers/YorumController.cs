using _18_filter_operation.Filters;
using _18_filter_operation.Models;
using Microsoft.AspNetCore.Mvc;

namespace _18_filter_operation.Controllers
{
    [PerformansFiltresi]
    public class YorumController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            System.Threading.Thread.Sleep(3500); // 1.5 saniye bekletme
            return View();
        }

        [HttpPost]
        [KufurEngelleyici]
        public IActionResult Kaydet(YorumModel model)
        {
            System.Threading.Thread.Sleep(500); // 1 saniye bekletme
            ViewBag.Mesaj = $"Teşekkürler {model.KullaniciAdi}, yorumunuz alındı: {model.YorumIcerigi}";
            return View("Index");
        }

        [HataYakalayici]
        public IActionResult HataPatlat()
        {
            int sayi1 = 10;
            int sayi2 = 0;
            int sonuc = sayi1 / sayi2;
            return View();
        }
    }
}
