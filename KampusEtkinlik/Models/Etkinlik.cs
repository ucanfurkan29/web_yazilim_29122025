namespace KampusEtkinlik.Models
{
    public class Etkinlik
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
        public string ResimUrl { get; set; } // Örn: "https://via.placeholder.com/150"
    }
}