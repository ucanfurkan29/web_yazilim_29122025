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


        public async Task<IActionResult> Index()
        {
            var movies =  await _tmbdService.GetPopularMoviesAsync();
            return View(movies);
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
