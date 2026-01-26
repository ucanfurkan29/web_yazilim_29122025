using Microsoft.AspNetCore.Mvc;

namespace _17_middleware2.Controllers
{
    /// <summary>
    /// API CONTROLLER
    /// ==============
    /// Bu controller, YetkiKontrolMiddleware'i test etmek için kullanılır.
    /// 
    /// /api/* yolları korumalıdır ve X-Api-Key header'ı gerektirir.
    /// 
    /// TEST ADIMLARI:
    /// ──────────────
    /// 1. Tarayıcıda /api/test adresine git
    ///    → 401 Unauthorized hatası alacaksın
    /// 
    /// 2. Postman veya curl ile dene:
    ///    curl -H "X-Api-Key: gizli-api-anahtari-12345" https://localhost:PORT/api/test
    ///    → Başarılı yanıt alacaksın
    /// 
    /// 3. Yanlış API key ile dene:
    ///    curl -H "X-Api-Key: yanlis-key" https://localhost:PORT/api/test
    ///    → 403 Forbidden hatası alacaksın
    /// </summary>
    [Route("api/[action]")]
    public class ApiController : Controller
    {
        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// API Test Endpoint
        /// URL: /api/test
        /// 
        /// Bu endpoint'e erişmek için geçerli API key gerekli!
        /// </summary>
        public IActionResult Test()
        {
            _logger.LogInformation("API Test endpoint'ine başarıyla erişildi!");

            return Json(new
            {
                basarili = true,
                mesaj = "🎉 Tebrikler! API'ye başarıyla eriştin!",
                zaman = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                bilgi = "Bu endpoint YetkiKontrolMiddleware tarafından korunuyor."
            });
        }

        /// <summary>
        /// Kullanıcı Listesi Endpoint
        /// URL: /api/kullanicilar
        /// </summary>
        public IActionResult Kullanicilar()
        {
            var kullanicilar = new[]
                     {
      new { Id = 1, Ad = "Ahmet", Soyad = "Yılmaz", Email = "ahmet@example.com" },
      new { Id = 2, Ad = "Ayşe", Soyad = "Kaya", Email = "ayse@example.com" },
                new { Id = 3, Ad = "Mehmet", Soyad = "Demir", Email = "mehmet@example.com" }
            };

            return Json(new
            {
                basarili = true,
                toplamKayit = kullanicilar.Length,
                veriler = kullanicilar
            });
        }

        /// <summary>
        /// Sunucu Durumu Endpoint
        /// URL: /api/durum
        /// </summary>
        public IActionResult Durum()
        {
            return Json(new
            {
                durum = "Çalışıyor",
                sunucuZamani = DateTime.Now,
                ortam = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                dotnetSurumu = Environment.Version.ToString()
            });
        }
    }
}
