using _11_models_binding.Models;
using _11_models_binding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _11_models_binding.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult HomePage()
        {
            Kisi kisi = new Kisi()
            {
                Ad = "Ahmet",
                Soyad = "Yýlmaz",
                Yas = 30
            };
            Adres adres = new Adres()
            {
                AdresTanim = "Ýstiklal Cad. No:10",
                Sehir = "Ýstanbul"
            };

            return View(kisi);
        }

        public IActionResult HomePage2()
        {
            Kisi kisi = new Kisi()
            {
                Ad = "Ahmet",
                Soyad = "Yýlmaz",
                Yas = 30
            };
            Adres adres = new Adres()
            {
                AdresTanim = "Ýstiklal Cad. No:10",
                Sehir = "Ýstanbul"
            };

            homePageViewModel viewModel = new homePageViewModel();

            viewModel.AdresNesnesi = adres;
            viewModel.KisiNesnesi = kisi;
            
            return View(viewModel);
        }
    }
}
