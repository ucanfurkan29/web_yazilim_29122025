namespace _17_middleware2.Middlewares
{
    /// <summary>
    /// LOGLAMA MİDDLEWARE
    /// ==================
    /// Bu middleware, gelen her HTTP isteğini ve dönen yanıtı loglar.
    /// Middleware, request pipeline'da bir zincir gibi çalışır.
    /// Her middleware kendinden sonrakini çağırır ve kontrol geri döndüğünde
    /// kendi işlemlerini yapabilir.
    /// 
    /// Çalışma Sırası:
    /// İstek Gelir → Middleware 1 → Middleware 2 → Controller → Middleware 2 → Middleware 1 → Yanıt Gider
    /// </summary>
    public class LoglamaMiddleware
    {
        // _next: Bir sonraki middleware'i temsil eder
        // RequestDelegate, HTTP isteğini işleyen bir metot imzasıdır
        private readonly RequestDelegate _next;

        // ILogger: .NET'in built-in loglama sistemi
        private readonly ILogger<LoglamaMiddleware> _logger;

        /// <summary>
        /// Constructor - Middleware oluşturulurken çağrılır (uygulama başlangıcında 1 kez)
        /// </summary>
        /// <param name="next">Pipeline'daki bir sonraki middleware</param>
        /// <param name="logger">Loglama servisi (Dependency Injection ile gelir)</param>
        public LoglamaMiddleware(RequestDelegate next, ILogger<LoglamaMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invoke veya InvokeAsync metodu - HER İSTEKTE çalışır
        /// Bu metot middleware'in kalbidir. Tüm iş burada yapılır.
        /// </summary>
        /// <param name="context">HTTP bağlamı - istek ve yanıt bilgilerini içerir</param>
        public async Task InvokeAsync(HttpContext context)
        {
            // ============================================
            // İSTEK GELDİĞİNDE YAPILACAKLAR (Request)
            // ============================================

            // İstek bilgilerini topluyoruz
            var istekYolu = context.Request.Path;     // Örn: /Home/Index
            var istekMetodu = context.Request.Method; // Örn: GET, POST
            var istekZamani = DateTime.Now;           // İstek zamanı
            var kullaniciIP = context.Connection.RemoteIpAddress;    // Kullanıcının IP adresi
            var tarayiciBilgisi = context.Request.Headers["User-Agent"].ToString(); // Tarayıcı bilgisi

            // İsteği logluyoruz
            _logger.LogInformation(
              "═══════════════════════════════════════════════════════════════");
            _logger.LogInformation(
          "📥 YENİ İSTEK GELDİ");
            _logger.LogInformation(
            $"   🕐 Zaman: {istekZamani:dd.MM.yyyy HH:mm:ss}");
            _logger.LogInformation(
               $"   🌐 IP Adresi: {kullaniciIP}");
            _logger.LogInformation(
  $"   📍 Yol: {istekMetodu} {istekYolu}");
            _logger.LogInformation(
                $"   🖥️ Tarayıcı: {tarayiciBilgisi?.Substring(0, Math.Min(50, tarayiciBilgisi?.Length ?? 0))}...");

            // ============================================
            // SIRADAKI MIDDLEWARE'E GEÇ
            // ============================================
            // Bu satır çok önemli! Eğer _next() çağrılmazsa,
            // istek burada durur ve controller'a ulaşamaz!
            await _next(context);

            // ============================================
            // YANIT DÖNERKEN YAPILACAKLAR (Response)
            // ============================================
            // Controller işini bitirdi, yanıt geri dönüyor

            var yanitDurumKodu = context.Response.StatusCode;        // Örn: 200, 404, 500
            var yanitZamani = DateTime.Now;

            // Yanıtı logluyoruz
            _logger.LogInformation(
               $"📤 YANIT GÖNDERİLDİ");
            _logger.LogInformation(
                $"✅ Durum Kodu: {yanitDurumKodu}");
            _logger.LogInformation(
                   $"   ⏱️ Toplam Süre: {(yanitZamani - istekZamani).TotalMilliseconds:F2} ms");
            _logger.LogInformation(
      "═══════════════════════════════════════════════════════════════\n");
        }
    }
}
