namespace MovieProject.ViewModels
{
    public class ApiResult<T>
    {
        public int Page { get; set; }
        public List<T> Results { get; set; }
        public int Total_Pages { get; set; }
    }
}
