using Microsoft.AspNetCore.Mvc;

namespace _05_viewData_viewBag_tempData.Controllers
{
    public class SayacController : Controller
    {
        // Static değişken: Uygulama çalıştığı sürece hafızada kalır.
        public static int VeritabaniSayaci = 0;

        public IActionResult Index()
        {
            ViewBag.Sayac = VeritabaniSayaci;
            return View();
        }

        // YANLIŞ YÖNTEM: Return View (F5 Sorunu yaratır)
        [HttpPost]
        public IActionResult SayacArtirYanlis()
        {
            VeritabaniSayaci++;
            ViewBag.Mesaj = "Yanlış Yöntem! (Return View). F5 yaparsan sayı yine artar.";
            ViewBag.Renk = "danger";
            ViewBag.Sayac = VeritabaniSayaci;
            return View("Index");
        }

        // DOĞRU YÖNTEM: Redirect (PRG Pattern)
        [HttpPost]
        public IActionResult SayacArtirDogru()
        {
            VeritabaniSayaci++;
            TempData["Mesaj"] = "Doğru Yöntem! (Redirect). F5 yaparsan hiçbir şey olmaz.";
            TempData["Renk"] = "success";
            return RedirectToAction("Index");
        }
    }
}
