namespace _16_middleware.Middlewares
{
    public class BakimMiddleware
    {
        private readonly RequestDelegate _next;
        public BakimMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //her istekte çalışacak metot
        public async Task Invoke(HttpContext context, IConfiguration configuration)
        {
            // appsettings.json dosyasından bakım durumu bilgisini al
            bool bakimVarMi = configuration.GetValue<bool>("SiteBakimdaMi");

            if (bakimVarMi)
            {
                // Site bakımda ise 503 hatası döndür
                context.Response.StatusCode = 503; // Service Unavailable
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync("<h1>Site Bakımda</h1>");
            }
            else
            {
                // Bir sonraki middleware'e geçiş yap
                await _next(context);
            }
        }
    }
}
