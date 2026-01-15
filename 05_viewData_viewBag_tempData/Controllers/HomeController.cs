using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _05_viewData_viewBag_tempData.Controllers
{
    public class HomeController : Controller
    {
        //Index Action Method - Veri Taþýma Yöntemleri
        //Controllerdan View'a veri taþýmak için üç yaygýn yöntem vardýr: ViewData, ViewBag ve TempData.
        // - ViewBag: Dinamik bir nesne olarak çalýþýr ve ViewData'nýn üzerinde bir sarmalayýcýdýr. ViewBag, ViewData ile ayný verileri taþýr ancak daha okunabilir bir sözdizimi sunar.
        // - ViewData: Bir sözlük (dictionary) yapýsýdýr ve anahtar-deðer çiftleri þeklinde veri taþýr. ViewData, verileri Controller'dan View'a taþýmak için kullanýlýr.
        // - TempData: Geçici veri depolamak için kullanýlýr ve bir istekten diðerine veri taþýmak için uygundur. TempData, genellikle yönlendirmeler (redirects) arasýnda veri taþýmak için kullanýlýr.
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NormalGosterim()
        {
            // 1. ViewBag (Dinamik Nesne)
            ViewBag.Mesaj = "Merhaba, ViewBag ile veri taþýma!";

            // 2. ViewData (Sözlük Yapýsý)
            ViewData["Mesaj2"] = "Merhaba, ViewData ile veri taþýma!";

            // 3. TempData (Geçici Veri Depolama)
            TempData["Mesaj"] = "Merhaba, TempData ile veri taþýma!";

            return View();
        }

        public IActionResult VeriYukleVeYonlendir()
        {
            ViewBag.Mesaj = "ViewBag: Beni göremeyeceksin, Çünkü öleceðim :(";
            ViewData["Mesaj"] = "ViewData: Beni de göremeyeceksin, Çünkü ben de öleceðim :(";

            //TempData, yönlendirme (redirect) iþlemi sonrasýnda bile veriyi korur.
            TempData["Mesaj"] = "TempData: Redirect yapýldý ama Beni görebileceksin, Çünkü ben hayatta kalacaðým! :)";

            //HedefSayfa isimli Action'a yönlendirme yapýyoruz. (Yeni bir HTTP isteði oluþturuyor.)
            return View("HedefSayfa");

            //Bu þekilde yaparsak yönlendirme olmaz, ayný istek içinde kalýrýz. Yani ViewBag ve ViewData verileri de görünür olur.
            //return View("HedefSayfa"); 
        }
        public IActionResult HedefSayfa()
        {
            return View();
        }
    }
}
