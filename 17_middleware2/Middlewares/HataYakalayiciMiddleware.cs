namespace _17_middleware2.Middlewares
{
    /// <summary>
    /// GLOBAL HATA YAKALAMA MİDDLEWARE
    /// ================================
    /// Bu middleware, uygulamanın herhangi bir yerinde oluşan
    /// hataları yakalar ve kullanıcıya güzel bir hata sayfası gösterir.
    /// 
    /// Faydaları:
    /// - Kullanıcıya teknik hata detayları gösterilmez (güvenlik)
    /// - Tüm hatalar merkezi bir yerden loglanır
    /// - Özel hata sayfaları gösterilebilir
    /// 
    /// DİKKAT: Bu middleware pipeline'ın EN BAŞINA eklenmelidir!
    /// Böylece sonraki tüm middleware'lerdeki hataları yakalayabilir.
    /// </summary>
    public class HataYakalayiciMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HataYakalayiciMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public HataYakalayiciMiddleware(
      RequestDelegate next,
        ILogger<HataYakalayiciMiddleware> logger,
            IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // ============================================
                // TRY BLOĞU: Normal akış
                // ============================================
                // Bir sonraki middleware'e geç
                // Herhangi bir yerde hata olursa catch bloğuna düşer
                await _next(context);
            }
            catch (Exception ex)
            {
                // ============================================
                // CATCH BLOĞU: Hata yakalandı
                // ============================================

                // Hatayı detaylı şekilde logla
                _logger.LogError(ex,
              "💥 HATA YAKALANDI!" +
             $"\n   📍 Yol: {context.Request.Path}" +
                 $"\n   📝 Hata: {ex.Message}" +
             $"\n   🔍 Tip: {ex.GetType().Name}");

                // Response'u temizle (önceki yazılan içerik varsa)
                context.Response.Clear();
                context.Response.StatusCode = 500;
                context.Response.ContentType = "text/html; charset=utf-8";

                // ============================================
                // ORTAMA GÖRE HATA MESAJI
                // ============================================
                // Development ortamında detaylı hata göster
                // Production'da genel mesaj göster
                if (_environment.IsDevelopment())
                {
                    // Geliştirici ortamı - detaylı hata
                    await context.Response.WriteAsync($@"
                       <!DOCTYPE html>
                        <html>
                       <head>
                         <title>Hata Oluştu (Development)</title>
                          <style>
                       body {{ font-family: 'Segoe UI', Arial; padding: 20px; background: #1e1e1e; color: #fff; }}
                    .container {{ max-width: 900px; margin: 0 auto; }}
                        h1 {{ color: #ff6b6b; }}
                       .error-box {{ background: #2d2d2d; padding: 20px; border-radius: 8px; margin: 20px 0; }}
                                  .error-type {{ color: #ffd93d; font-size: 14px; }}
                           .error-message {{ color: #ff6b6b; font-size: 18px; margin: 10px 0; }}
                       .stack-trace {{ background: #1a1a1a; padding: 15px; border-radius: 4px; 
                           overflow-x: auto; font-family: 'Consolas', monospace; font-size: 12px; color: #aaa; white-space: pre-wrap; }}
                            .request-info {{ background: #2d2d2d; padding: 15px; border-radius: 8px; }}
                        .request-info h3 {{ color: #6bcb77; margin-top: 0; }}
                               .info-item {{ margin: 5px 0; }}
                         .label {{ color: #888; }}
                        </style>
                               </head>
                       <body>
                            <div class='container'>
                              <h1>💥 Hata Oluştu!</h1>
                     <p>Aşağıda hata detaylarını görebilirsiniz (Bu sayfa sadece Development ortamında görünür)</p>
   
                       <div class='error-box'>
                               <div class='error-type'>🔍 Hata Tipi: {ex.GetType().FullName}</div>
                                <div class='error-message'>📝 Mesaj: {ex.Message}</div>
                               </div>

                            <div class='request-info'>
                       <h3>📋 İstek Bilgileri</h3>
                         <div class='info-item'><span class='label'>Yol:</span> {context.Request.Path}</div>
                           <div class='info-item'><span class='label'>Metot:</span> {context.Request.Method}</div>
                           <div class='info-item'><span class='label'>Query String:</span> {context.Request.QueryString}</div>
                           <div class='info-item'><span class='label'>Zaman:</span> {DateTime.Now:dd.MM.yyyy HH:mm:ss}</div>
                         </div>

                         <h3>📚 Stack Trace</h3>
                      <div class='stack-trace'>{ex.StackTrace}</div>
     
                       {(ex.InnerException != null ? $@"
                              <h3>🔗 Inner Exception</h3>
                    <div class='error-box'>
                        <div class='error-type'>{ex.InnerException.GetType().Name}</div>
                       <div class='error-message'>{ex.InnerException.Message}</div>
                        </div>" : "")}
                           </div>
                     </body>
              </html>");
                }
                else
                {
                    // Production ortamı - genel mesaj
                    await context.Response.WriteAsync($@"
                     <!DOCTYPE html>
                      <html>
                       <head>
                             <title>Bir Hata Oluştu</title>
                            <style>
                                body {{ 
                     font-family: 'Segoe UI', Arial; 
                      padding: 50px; 
                            text-align: center;
                      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                        min-height: 100vh;
                         margin: 0;
                       display: flex;
                        align-items: center;
                      justify-content: center;
                      }}
                          .error-container {{ 
                             background: white; 
                     padding: 60px; 
                         border-radius: 16px;
                         box-shadow: 0 20px 60px rgba(0,0,0,0.3);
                            max-width: 500px;
                          }}
                                  h1 {{ color: #e74c3c; margin: 0 0 20px 0; font-size: 72px; }}
                     h2 {{ color: #333; margin: 0 0 15px 0; }}
                       p {{ color: #666; line-height: 1.6; }}
                           a {{ 
                      color: #667eea; 
                       text-decoration: none;
                          display: inline-block;
                           margin-top: 20px;
                          padding: 12px 30px;
                         border: 2px solid #667eea;
                       border-radius: 30px;
                     transition: all 0.3s;
                      }}
                       a:hover {{ 
                        background: #667eea;
                             color: white;
                    }}
                            </style>
                         </head>
                              <body>
                     <div class='error-container'>
                         <h1>😔</h1>
                         <h2>Bir Hata Oluştu</h2>
                          <p>Üzgünüz, bir şeyler yanlış gitti. Teknik ekibimiz bilgilendirildi.</p>
                             <p>Lütfen daha sonra tekrar deneyin.</p>
                        <a href='/'>🏠 Ana Sayfaya Dön</a>
                          </div>
                        </body>
                       </html>");
                }
            }
        }
    }
}
