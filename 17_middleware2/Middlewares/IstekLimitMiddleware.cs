using System.Collections.Concurrent;

namespace _17_middleware2.Middlewares
{
    /// <summary>
    /// İSTEK LİMİT (RATE LIMITING) MİDDLEWARE
    /// =======================================
    /// Bu middleware, belirli bir süre içinde çok fazla istek yapan 
    /// kullanıcıları engeller (DoS saldırılarına karşı koruma).
    /// 
    /// Örnek: Her IP adresi dakikada maksimum 60 istek yapabilir.
    /// 
    /// NOT: .NET 7+ ile birlikte Microsoft'un built-in Rate Limiting
    /// middleware'i var. Bu örnek eğitim amaçlıdır.
    /// </summary>
    public class IstekLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<IstekLimitMiddleware> _logger;

        // ============================================
        // AYARLAR
        // ============================================
        private const int DAKIKADA_MAKSIMUM_ISTEK = 60;      // Dakikada max istek sayısı
        private const int ENGEL_SURESI_SANIYE = 60;          // Engelleme süresi

        // ============================================
        // İSTEK SAYACI (Thread-Safe)
        // ============================================
        // ConcurrentDictionary: Birden fazla thread aynı anda erişse bile güvenli çalışır
        // Key: IP adresi, Value: (İstek Sayısı, Son Sıfırlama Zamanı)
        private static readonly ConcurrentDictionary<string, (int istekSayisi, DateTime sonSifirlama)>
   _istekSayaci = new();

        // Engellenen IP'ler ve engel bitiş zamanları
        private static readonly ConcurrentDictionary<string, DateTime>
       _engellenenIpler = new();

        public IstekLimitMiddleware(RequestDelegate next, ILogger<IstekLimitMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // ============================================
            // KULLANICI IP ADRESİNİ AL
            // ============================================
            var ipAdresi = context.Connection.RemoteIpAddress?.ToString() ?? "bilinmiyor";

            // ============================================
            // ENGEL KONTROLÜ
            // ============================================
            // Bu IP engelli mi kontrol et
            if (_engellenenIpler.TryGetValue(ipAdresi, out var engelBitisZamani))
            {
                if (DateTime.Now < engelBitisZamani)
                {
                    // Hala engelli
                    var kalanSure = (engelBitisZamani - DateTime.Now).TotalSeconds;
                    _logger.LogWarning($"🚫 Engelli IP erişim denemesi: {ipAdresi} | Kalan süre: {kalanSure:F0} saniye");

                    context.Response.StatusCode = 429; // Too Many Requests
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Append("Retry-After", ((int)kalanSure).ToString());
                    await context.Response.WriteAsync($@"
      <html>
        <head><title>Çok Fazla İstek</title></head>
               <body style='font-family: Arial; text-align: center; padding: 50px;'>
      <h1>🚫 Çok Fazla İstek!</h1>
     <p>Dakikada maksimum {DAKIKADA_MAKSIMUM_ISTEK} istek yapabilirsiniz.</p>
  <p>Lütfen <strong>{kalanSure:F0} saniye</strong> sonra tekrar deneyin.</p>
          </body>
     </html>");
                    return;
                }
                else
                {
                    // Engel süresi dolmuş, kaldır
                    _engellenenIpler.TryRemove(ipAdresi, out _);
                }
            }

            // ============================================
            // İSTEK SAYACINI GÜNCELLE
            // ============================================
            var simdi = DateTime.Now;

            var (istekSayisi, sonSifirlama) = _istekSayaci.GetOrAdd(ipAdresi, (0, simdi));

            // 1 dakika geçtiyse sayacı sıfırla
            if ((simdi - sonSifirlama).TotalMinutes >= 1)
            {
                istekSayisi = 0;
                sonSifirlama = simdi;
            }

            istekSayisi++;

            // Sayacı güncelle
            _istekSayaci[ipAdresi] = (istekSayisi, sonSifirlama);

            // ============================================
            // LİMİT KONTROLÜ
            // ============================================
            if (istekSayisi > DAKIKADA_MAKSIMUM_ISTEK)
            {
                // Limite ulaşıldı, IP'yi engelle
                _engellenenIpler[ipAdresi] = simdi.AddSeconds(ENGEL_SURESI_SANIYE);

                _logger.LogWarning(
                   $"⛔ IP ENGELLENDİ: {ipAdresi} | " +
              $"İstek Sayısı: {istekSayisi} | " +
             $"Limit: {DAKIKADA_MAKSIMUM_ISTEK}");

                context.Response.StatusCode = 429;
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($@"
          <html>
    <head><title>Çok Fazla İstek</title></head>
    <body style='font-family: Arial; text-align: center; padding: 50px;'>
            <h1>⛔ Engellendiniz!</h1>
        <p>Çok fazla istek yaptığınız için {ENGEL_SURESI_SANIYE} saniye engellendiniz.</p>
      </body>
           </html>");
                return;
            }

            // ============================================
            // LİMİT AŞILMADIYSA DEVAM ET
            // ============================================
            // Response header'a kalan istek hakkını ekle
            var kalanHak = DAKIKADA_MAKSIMUM_ISTEK - istekSayisi;
            context.Response.Headers.Append("X-RateLimit-Limit", DAKIKADA_MAKSIMUM_ISTEK.ToString());
            context.Response.Headers.Append("X-RateLimit-Remaining", kalanHak.ToString());

            await _next(context);
        }
    }
}
