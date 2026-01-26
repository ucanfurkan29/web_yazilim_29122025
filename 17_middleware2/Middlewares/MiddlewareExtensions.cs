namespace _17_middleware2.Middlewares
{
    /// <summary>
    /// MÝDDLEWARE EXTENSÝON METODLARI
    /// ===============================
    /// Bu sýnýf, middleware'leri daha temiz bir þekilde eklemek için
    /// extension metodlar saðlar.
    /// 
    /// Extension Method Nedir?
    /// - Mevcut bir sýnýfa yeni metodlar eklememizi saðlar
    /// - Ýlk parametre "this" anahtar kelimesi ile baþlar
    /// - Kullanýmý: app.UseLogging() þeklinde olur
    /// 
    /// Neden Extension Method Kullanýyoruz?
    /// - Kod daha okunabilir olur: app.UseLogging() vs app.UseMiddleware<LoglamaMiddleware>()
    /// - Middleware'e parametre geçmek kolaylaþýr
    /// - ASP.NET Core'un kendi middleware'leri de böyle çalýþýr
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Loglama middleware'ini ekler.
        /// Kullaným: app.UseLogging();
        /// </summary>
        public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
        {
            // UseMiddleware<T>() metodu, middleware'i pipeline'a ekler
            return app.UseMiddleware<LoglamaMiddleware>();
        }

        /// <summary>
        /// Performans ölçüm middleware'ini ekler.
        /// Kullaným: app.UsePerformans();
        /// </summary>
        public static IApplicationBuilder UsePerformans(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PerformansMiddleware>();
        }

        /// <summary>
        /// Yetki kontrol middleware'ini ekler.
        /// Kullaným: app.UseYetkiKontrol();
        /// </summary>
        public static IApplicationBuilder UseYetkiKontrol(this IApplicationBuilder app)
        {
            return app.UseMiddleware<YetkiKontrolMiddleware>();
        }

        /// <summary>
        /// Ýstek limit (rate limiting) middleware'ini ekler.
        /// Kullaným: app.UseIstekLimit();
        /// </summary>
        public static IApplicationBuilder UseIstekLimit(this IApplicationBuilder app)
        {
            return app.UseMiddleware<IstekLimitMiddleware>();
        }

        /// <summary>
        /// Hata yakalayýcý middleware'ini ekler.
        /// Kullaným: app.UseHataYakalayici();
        /// DÝKKAT: Pipeline'ýn en baþýna eklenmelidir!
        /// </summary>
        public static IApplicationBuilder UseHataYakalayici(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HataYakalayiciMiddleware>();
        }
    }
}
