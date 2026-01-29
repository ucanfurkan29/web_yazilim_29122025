namespace MovieProject.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int MovieId { get; set; } // TMDB Movie ID
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key to AppUser
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
