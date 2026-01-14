//Uyguluma inþaatçýsýný oluþturuyor

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "blogDetails", // rota ismi, isteðe baðlý
    pattern: "detaylar/{id}", // özel rota ayarlayabiliriz
    defaults: new { controller = "Blog", action = "Details" }, //varsayýlan controller ve action
    constraints: new { id = @"\d+" } // /d sadece rakamlarý kabul eder, + bir veya daha fazla rakam anlamýna gelir
    );

app.MapControllerRoute(
    name: "AboutRoute",
    pattern: "hakkimda",
    defaults: new { controller = "Home", action = "About" }
    );


app.Run();
