using MovieProject.Models;

namespace MovieProject.ViewModels
{
    public class MovieDetailDTO
    {
        //API den gelen film bilgisini tutan property
        public MovieDetailViewModel MovieDetail { get; set; }

        public List<Review> Reviews { get; set; }

        //Kullanıcının o filme ait izleme günlüğü bilgisini tutan property
        public UserMovieLog CurrentUserLog { get; set; }

        //Kullanıcının yeni yorum ve puan girişi için propertyler
        public string NewCommentContent { get; set; }
        public int NewRating { get; set; }
    }
}
    