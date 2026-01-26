namespace _17_middleware2.Middlewares
{
    /// <summary>
    /// PERFORMANS ÖLÇÜM MİDDLEWARE
    /// ============================
    /// Bu middleware her isteğin ne kadar sürdüğünü ölçer.
    /// Yavaş istekleri tespit etmek için kullanılır.
    /// 
    /// Kullanım Alanları:
    /// - Performans sorunlarını tespit etme
    /// - Yavaş endpoint'leri bulma
    /// - Optimizasyon gerektiren yerleri belirleme
    /// </summary>
    public class PerformansMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerformansMiddleware> _logger;

        // Uyarı eşiği: Bu süreden uzun süren istekler için uyarı verilir
        private const int YAVAS_ISTEK_ESIGI_MS = 500; // 500 milisaniye

        public PerformansMiddleware(RequestDelegate next, ILogger<PerformansMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // ============================================
            // ZAMANLAYICIYI BAŞLAT
            // ============================================
            // Stopwatch: Yüksek hassasiyetli süre ölçer
            var zamanlayici = System.Diagnostics.Stopwatch.StartNew();

            // Pipeline'daki bir sonraki middleware'e geç
            // (Bu, controller'a kadar gidip geri dönecek)
            await _next(context);

            // ============================================
            // ZAMANLAYICIYI DURDUR VE SONUCU DEĞERLENDIR
            // ============================================
            zamanlayici.Stop();

            var gecenSureMs = zamanlayici.ElapsedMilliseconds;
            var istekYolu = context.Request.Path;

            // Yavaş istekleri uyarı olarak logla
            if (gecenSureMs > YAVAS_ISTEK_ESIGI_MS)
            {
                // LogWarning: Sarı renkli uyarı logu
                _logger.LogWarning(
        "⚠️ YAVAS İSTEK TESPİT EDİLDİ! " +
     $"Yol: {istekYolu} | " +
   $"Süre: {gecenSureMs} ms | " +
        $"Eşik: {YAVAS_ISTEK_ESIGI_MS} ms");
            }
            else
            {
                // Normal istekleri debug olarak logla
                _logger.LogDebug(
                 $"⚡ İstek tamamlandı: {istekYolu} | Süre: {gecenSureMs} ms");
            }

            // ============================================
            // RESPONSE HEADER'A SÜRE BİLGİSİ EKLE
            // ============================================
            // Tarayıcının DevTools'unda bu header'ı görebilirsin
            // NOT: Yanıt başlamadan önce header eklenmelidir,
            // bu yüzden bazı durumlarda çalışmayabilir
            if (!context.Response.HasStarted)
            {
                context.Response.Headers.Append("X-Islem-Suresi-Ms", gecenSureMs.ToString());
            }
        }
    }
}
