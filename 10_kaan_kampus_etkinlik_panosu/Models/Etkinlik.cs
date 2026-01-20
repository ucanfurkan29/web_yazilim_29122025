using System.ComponentModel.DataAnnotations;

namespace _10_kaan_kampus_etkinlik_panosu.Models
{
    public class Etkinlik
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Etkinlik adı zorunlu.")]
        public string Ad { get; set; } = "";

        [Required(ErrorMessage = "Tarih zorunlu.")]
        [DataType(DataType.Date)]
        public DateTime Tarih { get; set; }

        [Required(ErrorMessage = "Açıklama zorunlu.")]
        public string Aciklama { get; set; } = "";

        [Display(Name = "Resim URL")]
        public string? ResimUrl { get; set; }
    }
}
