namespace _19_web_api.Models
{
    public class Urun
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Thumbnail { get; set; }
        public string Category { get; set; }
        public double Rating { get; set; }
    }
}
