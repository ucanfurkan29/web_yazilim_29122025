namespace _17_middleware2.Middlewares
{
    /// <summary>
    /// YETKİ KONTROL MİDDLEWARE
    /// ========================
    /// Bu middleware, belirli sayfalara erişim için API key kontrolü yapar.
    /// 
    /// API Key şu yollarla gönderilebilir:
    /// 1. Header: X-Api-Key: gizli-api-anahtari-12345
    /// 2. Query String: ?apiKey=gizli-api-anahtari-12345
    /// 
    /// Senaryo: /api/* ve /admin/* altındaki tüm endpoint'ler için API key zorunlu
    /// </summary>
    public class YetkiKontrolMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<YetkiKontrolMiddleware> _logger;
        private readonly IConfiguration _configuration;

        // Koruma altına alınacak yollar
        private readonly string[] _korunanyollar = { "/api", "/admin" };

        public YetkiKontrolMiddleware(RequestDelegate next, ILogger<YetkiKontrolMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var istekYolu = context.Request.Path.Value?.ToLower() ?? "";

            // ============================================
            // KORUNAN YOL KONTROLÜ
            // ============================================
            // İstek korunan bir yola mı gidiyor kontrol et
            bool korunanYolaMiGidiyor = _korunanyollar.Any(yol => istekYolu.StartsWith(yol, StringComparison.OrdinalIgnoreCase));

            if (korunanYolaMiGidiyor)
            {
                _logger.LogInformation($"🔒 Korunan yola erişim denemesi: {istekYolu}");

                // ============================================
                // API KEY'İ AL (Header VEYA Query String)
                // ============================================
                // Önce Header'dan kontrol et
                var apiKey = context.Request.Headers["X-Api-Key"].FirstOrDefault();

                // Header'da yoksa Query String'den kontrol et
                if (string.IsNullOrEmpty(apiKey))
                {
                    apiKey = context.Request.Query["apiKey"].FirstOrDefault();

                    if (!string.IsNullOrEmpty(apiKey))
                    {
                        _logger.LogInformation("🔑 API Key query string'den alındı");
                    }
                }
                else
                {
                    _logger.LogInformation("🔑 API Key header'dan alındı");
                }

                // appsettings.json'dan geçerli API key'i al
                var gecerliApiKey = _configuration["ApiSettings:ApiKey"];

                // API key kontrolü
                if (string.IsNullOrEmpty(apiKey))
                {
                    // API key gönderilmemiş
                    _logger.LogWarning($"⛔ API Key eksik! Yol: {istekYolu}");

                    context.Response.StatusCode = 401; // Unauthorized
                    context.Response.ContentType = "application/json; charset=utf-8";
                    await context.Response.WriteAsync("{\"hata\": \"API Key gerekli! Header'a 'X-Api-Key' ekleyin veya URL'e '?apiKey=...' parametresi ekleyin.\"}");
                    return; // Pipeline'ı burada durdur, devam etme!
                }

                if (apiKey != gecerliApiKey)
                {
                    // API key yanlış
                    _logger.LogWarning($"⛔ Geçersiz API Key! Yol: {istekYolu}");

                    context.Response.StatusCode = 403; // Forbidden
                    context.Response.ContentType = "application/json; charset=utf-8";
                    await context.Response.WriteAsync("{\"hata\": \"Geçersiz API Key!\"}");
                    return; // Pipeline'ı burada durdur!
                }

                // API key doğru
                _logger.LogInformation($"✅ API Key doğrulandı! Yol: {istekYolu}");
            }

            // ============================================
            // YETKİ VARSA DEVAM ET
            // ============================================
            // Korunan yol değilse veya yetki varsa devam et
            await _next(context);
        }
    }
}
