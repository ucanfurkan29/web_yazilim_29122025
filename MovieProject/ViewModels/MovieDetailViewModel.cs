namespace MovieProject.ViewModels
{
    public class MovieDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Poster_Path { get; set; }
        public string Backdrop_Path { get; set; }
        public double Vote_Average { get; set; }
        public string Release_Date { get; set; }
        public int Runtime { get; set; }
        public List<GenreViewModel> Genres { get; set; } = new();

        public string FullPosterPath => string.IsNullOrEmpty(Poster_Path)
            ? "https://via.placeholder.com/500x750?text=No+Image"
            : $"https://image.tmdb.org/t/p/w500{Poster_Path}";
        public string FullBackdropPath => string.IsNullOrEmpty(Backdrop_Path)
            ? "https://via.placeholder.com/1280x720?text=No+Image"
            : $"https://image.tmdb.org/t/p/original{Backdrop_Path}";

    }
    public class GenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
