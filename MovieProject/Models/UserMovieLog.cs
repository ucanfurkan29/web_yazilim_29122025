namespace MovieProject.Models
{
    public class UserMovieLog
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public bool IsWatched { get; set; }
        public bool IsWatchlist { get; set; }
        public int? UserRating { get; set; } //1-10 arası

        // Foreign Key to AppUser
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
