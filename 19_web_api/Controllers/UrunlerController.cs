using _19_web_api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace _19_web_api.Controllers
{
    public class UrunlerController : Controller
    {
        private readonly HttpClient _httpClient;
        public UrunlerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            //Api adresine git - tarayıcıya link yapıştırmak ile aynı şey
            var response = await _httpClient.GetAsync("https://dummyjson.com/products");

            //Başarılı mı? (200 OK)
            if (response.IsSuccessStatusCode)
            {
                //Gelen veriyi oku - JSON formatında
                //Şuan elimizde upuzun bir JSON stringi var
                var jsonVerisi = await response.Content.ReadAsStringAsync();

                //JSON stringini C# nesnesine dönüştür (Deserialize)
                var ayarlar = new JsonSerializerOptions
                {
                    //Büyük küçük harf duyarlılığını kaldır
                    PropertyNameCaseInsensitive = true
                };

                var apiCevabi = JsonSerializer.Deserialize<ApiYaniti>(jsonVerisi, ayarlar);

                return View(apiCevabi.Products);
            }
            else
            {
                //Hata durumunda boş liste döndür veya hata sayfasına da yönlendirilebilir
                return View(new List<Urun>());
            }
        }
    }
}
