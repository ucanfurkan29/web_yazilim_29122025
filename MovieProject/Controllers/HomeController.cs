using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieProject.Models;
using MovieProject.Services;
using System.Diagnostics;

namespace MovieProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly TmbdService _tmbdService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(TmbdService tmbdService, ILogger<HomeController> logger)
        {
            _tmbdService = tmbdService;
            _logger = logger;

        }

        public async Task<IActionResult> Index(int page = 1)
        {
            // Sayfa numarasýný 1-500 arasýnda sýnýrla
            page = Math.Clamp(page, 1, 500);

            var result = await _tmbdService.GetPopularMoviesAsync(page);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = 500;

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
