using _10_kaan_kampus_etkinlik_panosu.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _10_kaan_kampus_etkinlik_panosu.Controllers
{
    public class HomeController : Controller
    {
        public static List<Etkinlik> Etkinlikler { get; } = new()
        {
            new Etkinlik
            {
                Id = 1,
                Ad = "Bahar Þenliði",
                Tarih = DateTime.Today.AddDays(3),
                Aciklama = "Müzik ve Eðlence",
                ResimUrl = ""
            },
            new Etkinlik
            {
                Id = 2,
                Ad = "Yazýlým Semineri",
                Tarih = DateTime.Today.AddDays(10),
                Aciklama = "C# ve MVC konuþacaðýz",
                ResimUrl = ""
            }
        };

        public IActionResult Index()
        {
            ViewBag.Title = "Kampüs Etkinlikleri";
            return View(Etkinlikler.OrderBy(x => x.Tarih).ToList());
        }

        public IActionResult Detay(int id)
        {
            ViewData["Title"] = "Etkinlik Detayý";
            var etkinlik = Etkinlikler.FirstOrDefault(x => x.Id == id);
            if (etkinlik == null) return NotFound();
            return View(etkinlik);
        }

        public static int NextId()
        {
            return Etkinlikler.Count == 0 ? 1 : Etkinlikler.Max(x => x.Id) + 1;
        }

        public static void ClearAll()
        {
            Etkinlikler.Clear();
        }
    }
}

