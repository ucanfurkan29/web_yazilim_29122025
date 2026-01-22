using _14_dependency_injection.Models;
using _14_dependency_injection.Services.Abstract;
using _14_dependency_injection.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _14_dependency_injection.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOgrenciService _ogrenciService;
        public HomeController(IOgrenciService ogrenciService)
        {
            _ogrenciService = ogrenciService;
        }
        

        public IActionResult Index()
        {
            var ogrenciListesi = _ogrenciService.ListeyiGetir();
            return View(ogrenciListesi);
        }
    }
}
