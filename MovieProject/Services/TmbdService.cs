using MovieProject.ViewModels;
using System.Text.Json;

namespace MovieProject.Services
{
    public class TmbdService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public TmbdService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            var baseUrl = _configuration["TMDB:BaseUrl"];
            var apiKey = _configuration["TMDB:ApiKey"];

            _httpClient.BaseAddress = new Uri(baseUrl);
            //her isteğe apiKey ekliyoruz
        }

        //popüler filmleri getir
        public async Task<List<MovieViewModel>> GetPopularMoviesAsync()
        {
            var apiKey = _configuration["TMDB:ApiKey"];
            if(string.IsNullOrEmpty(apiKey)) return new List<MovieViewModel>();

            try
            {
                var response = await _httpClient.GetAsync($"movie/popular?api_key={apiKey}&language=tr-TR");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    //json verisini deserialize et(büyük küçük ayarları dikkate alma)
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    //jsonString ı ApiResult<MovieViewModel> türüne dönüştür
                    var data = JsonSerializer.Deserialize<ApiResult<MovieViewModel>>(jsonString, options);

                    return data?.Results ?? new List<MovieViewModel>();
                }
            }
            catch
            {
                return new List<MovieViewModel>(); //başarısızsa boş liste döner
            }
            return new List<MovieViewModel>(); //başarısızsa boş liste döner
        }

        public async Task<MovieDetailViewModel> GetMovieDetailAsync(int id)
        {
            var apiKey = _configuration["TMBD:ApiKey"];
            var response = await _httpClient.GetAsync($"movie/{id}?api_key={apiKey}&language=tr-TR");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var data = JsonSerializer.Deserialize<MovieDetailViewModel>(jsonString, options);

                return data;
            }
            return null;
        }
        public async Task<List<MovieViewModel>> SearchMovieAsync(string query)
        {
            var apiKey = _configuration["TMBD:ApiKey"];
            var response = await _httpClient.GetAsync($"search/movie?api_key={apiKey}&language=tr-TR&query={query}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var data = JsonSerializer.Deserialize<ApiResult<MovieViewModel>>(jsonString, options);

                return data.Results;
            }
            return new List<MovieViewModel>();
        }
    }
}
