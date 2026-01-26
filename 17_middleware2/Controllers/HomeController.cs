using _17_middleware2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _17_middleware2.Controllers
{
    /// <summary>
    /// ANA CONTROLLER
    /// ===============
    /// Middleware'leri test etmek için çeþitli action'lar içerir.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Ana sayfa - Middleware bilgilerini gösterir
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// HATA TEST ACTION
        /// ????????????????
        /// Bu action kasýtlý olarak hata fýrlatýr.
        /// HataYakalayiciMiddleware'i test etmek için kullanýlýr.
        /// 
        /// Test: /Home/HataTest adresine git
        /// </summary>
        public IActionResult HataTest()
        {
            _logger.LogInformation("HataTest action çaðrýldý - Þimdi hata fýrlatýlacak!");

            // Kasýtlý olarak bir hata fýrlatýyoruz
            throw new Exception("Bu bir test hatasýdýr! HataYakalayiciMiddleware bunu yakalayacak.");
        }

        /// <summary>
        /// YAVAÞ ÝSTEK TEST ACTION
        /// ???????????????????????
        /// Bu action kasýtlý olarak yavaþ çalýþýr (2 saniye bekler).
        /// PerformansMiddleware'i test etmek için kullanýlýr.
        /// 
        /// Test: /Home/YavasIstek adresine git
        /// Beklenen: Console'da "YAVAÞ ÝSTEK TESPÝT EDÝLDÝ" uyarýsý
        /// </summary>
        public async Task<IActionResult> YavasIstek()
        {
            _logger.LogInformation("YavasIstek action baþladý - 2 saniye beklenecek...");

            // 2 saniye bekle (Performans middleware eþiði 500ms)
            await Task.Delay(2000);

            _logger.LogInformation("YavasIstek action tamamlandý!");

            return Content(@"
     <html>
    <head><title>Yavaþ Ýstek Testi</title></head>
       <body style='font-family: Arial; padding: 50px; text-align: center;'>
      <h1>?? Yavaþ Ýstek Tamamlandý!</h1>
               <p>Bu istek 2 saniye sürdü.</p>
       <p>Console'da 'YAVAÞ ÝSTEK TESPÝT EDÝLDÝ' uyarýsýný göreceksiniz.</p>
                  <p>Ayrýca Response Headers'da 'X-Islem-Suresi-Ms' deðerini kontrol edin.</p>
      <a href='/'>Ana Sayfa</a>
 </body>
     </html>
     ", "text/html");
        }

        /// <summary>
        /// NORMAL ÝSTEK TEST ACTION
        /// ????????????????????????
        /// Bu action hýzlý çalýþýr.
        /// Performans karþýlaþtýrmasý için kullanýlýr.
        /// 
        /// Test: /Home/HizliIstek adresine git
        /// </summary>
        public IActionResult HizliIstek()
        {
            return Content(@"
         <html>
    <head><title>Hýzlý Ýstek Testi</title></head>
       <body style='font-family: Arial; padding: 50px; text-align: center;'>
        <h1>? Hýzlý Ýstek Tamamlandý!</h1>
         <p>Bu istek çok hýzlý tamamlandý.</p>
    <p>Response Headers'da 'X-Islem-Suresi-Ms' deðerini kontrol edin.</p>
           <a href='/'>Ana Sayfa</a>
      </body>
                </html>
            ", "text/html");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
