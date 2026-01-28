namespace MovieProject.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Poster_Path { get; set; }
        public double Vote_Average { get; set; }
        public string Release_Date { get; set; }

        //Helper
        public string FullPosterPath => string.IsNullOrEmpty(Poster_Path)
            ? "https://via.placeholder.com/500x750?text=No+Image"
            : $"https://image.tmdb.org/t/p/w500{Poster_Path}";
    }
}
