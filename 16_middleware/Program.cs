using _16_middleware.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//bakim middleware'ini ekliyoruz
//en baþa ekliyoruz ki diðer middleware'ler çalýþmadan önce bakým durumu kontrol edilsin
app.UseMiddleware<BakimMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Https 'e zorlama middleware'ini ekliyoruz
app.UseHttpsRedirection();
// wwwroot klasöründeki statik dosyalarý sunmak için middleware ekliyoruz
app.UseStaticFiles();
// Yönlendirme middleware'ini ekliyoruz
app.UseRouting();
// kimlik kontrolü yapan middleware'i ekliyoruz(auth her zaman Routing middlewareinden sonra olmalý)
app.UseAuthorization();

// Varsayýlan rota yapýlandýrmasýný ekliyoruz middleware'e
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();


/*
 * Sýk kullanýlan bazý ASP.NET Core  'Middleware'leri ve görevleri:
 Middleware,        Görevi
UseStaticFiles,     "CSS, JS, Resim gibi dosylarýn tarayýcýya gönderilmesine izin verir. Bu olmazsa sitenizdeki resimler yüklenmez."
UseRouting,         Gelen URL'in (örn: /Urun/Detay/5) nereye gideceðini hesaplar.
UseAuthentication,  Kullanýcýnýn giriþ yapýp yapmadýðýný (Cookie/Token) kontrol eder.
UseAuthorization,   Giriþ yapan kullanýcýnýn o sayfaya yetkisi var mý (Admin mi?) kontrol eder.
UseCors,            Baþka sitelerin sizin API'nize eriþip eriþemeyeceðini belirler.
UseSession,         (Az önce gördük) Session mekanizmasýný devreye sokar.
 */