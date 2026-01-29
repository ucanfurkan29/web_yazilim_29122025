using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieProject.Data;
using MovieProject.Models;
using MovieProject.Services;
using MovieProject.ViewModels;

namespace MovieProject.Controllers
{
    public class MoviesController : Controller
    {
        private readonly TmbdService _tmbdService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public MoviesController(TmbdService tmbdService, ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _tmbdService = tmbdService;
            _context = context;
            _userManager = userManager;
        }

        //get: Movies/Detail/123123
        public async Task<IActionResult> Detail(int id)
        {
            //apiden film detaylarını aldık.
            var movie = await _tmbdService.GetMovieDetailAsync(id);
            if (movie == null) return NotFound();

            //veritabanından o filme ait yorumları aldık
            var reviews = await _context.Reviews.Include(r => r.AppUser)
                                                .Where(r => r.MovieId == id)
                                                .OrderByDescending(r => r.CreatedAt)
                                                .ToListAsync();

            //kullanıcının o filme ait yorumu var mı?
            UserMovieLog userLog = null;
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                userLog = await _context.UserMovieLogs
                                        .FirstOrDefaultAsync(x => x.AppUserId == userId && x.MovieId == id);
            }

            var viewModel = new MovieDetailDTO
            {
                MovieDetail = movie,
                Reviews = reviews,
                CurrentUserLog = userLog
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize] //sadece giriş yapmış kullanıcılar yorum ekleyebilir
        public async Task<IActionResult> AddComment(int movieId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return RedirectToAction("Detail", new { id = movieId });
            }
            var userId = _userManager.GetUserId(User);
            var review = new Review
            {
                MovieId = movieId,
                Content = content,
                AppUserId = userId,
                CreatedAt = DateTime.Now
            };
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return RedirectToAction("Detail", new { id = movieId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateState(int movieId,int rating, bool isWatched,string movieTitle)
        {
            var userId = _userManager.GetUserId(User);
            var log = await _context.UserMovieLogs
                                    .FirstOrDefaultAsync(x => x.AppUserId == userId && x.MovieId == movieId);
            if (log == null)
            {
                log = new UserMovieLog
                {
                    AppUserId = userId,
                    MovieId = movieId,
                    MovieTitle = movieTitle
                };
                _context.UserMovieLogs.Add(log);
            }

            if (log.MovieTitle != movieTitle)
            {
                log.MovieTitle = movieTitle;
            }
            log.UserRating = rating;
            log.IsWatched = isWatched;
            await _context.SaveChangesAsync();
            return RedirectToAction("Detail", new { id = movieId } );
        }
    }
}
