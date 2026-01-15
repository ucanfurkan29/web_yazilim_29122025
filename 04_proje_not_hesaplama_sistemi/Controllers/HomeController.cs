using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _04_proje_not_hesaplama_sistemi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
/*
Proje Görevi: Not Hesaplama Sistemi
Senaryo: Bir üniversite için öðrencilerin Vize ve Final notlarýný girerek ortalamalarýný ve geçme/kalma durumlarýný görebilecekleri basit bir web arayüzü tasarlamanýz isteniyor.

HOMECONTROLLER ÝÇERÝSÝNDE YAPILMAYACAK. YENÝ BÝR CONTROLLER OLUÞTURULACAK.

?? Ýsterler (Requirements)
1. Bölüm: Karþýlama ve Veri Giriþi (View -> Controller)
    Uygulama açýldýðýnda (örneðin: /Hesaplama/NotGiris adresinde), kullanýcýyý bir Form karþýlamalýdýr.
    Formda þu alanlar olmalýdýr:
        Öðrenci Adý Soyadý (Metin kutusu)
        Vize Notu (Sayýsal giriþ)
        Final Notu (Sayýsal giriþ)
        Bir "Hesapla" butonu ile form verileri sunucuya (Controller'a) gönderilmelidir (POST iþlemi).
2. Bölüm: Mantýksal Ýþlemler (Controller)
    Controller tarafýnda formdan gelen veriler yakalanmalýdýr.
    Ortalama Hesaplama: (Vize * 0.4) + (Final * 0.6) formülü kullanýlmalýdýr.
    Durum Kontrolü: Eðer ortalama 50 ve üzerindeyse "GEÇTÝ", altýndaysa "KALDI" bilgisi oluþturulmalýdýr.
3. Bölüm: Sonuç Ekraný (Controller -> View)
    Hesaplama yapýldýktan sonra kullanýcý farklý bir View'e veya ayný View'in sonuç bölümüne yönlendirilmelidir.
    Ekranda þu mesaj dinamik olarak yazdýrýlmalýdýr (Veri taþýma yöntemleri kullanýlarak: ViewBag, ViewData veya Model):
    "Sayýn [Öðrenci Adý], not ortalamanýz: [Ortalama]. Durum: [GEÇTÝ/KALDI]"
    Eðer öðrenci geçtiyse mesaj yeþil, kaldýysa kýrmýzý renkte gösterilmelidir (Basit CSS veya Bootstrap class kullanýmý).
 */