using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations; //Doğrulama (validation) öznitelikleri için

namespace _06_html_helper.Models
{
    public class User
    {
        [Required(ErrorMessage ="Name is required")] //Zorunlu alan kuralı
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required")] //Zorunlu alan kuralı
        [Range(1,120, ErrorMessage ="Age must be between 1 and 120")] //Aralık Kontrolü
        public int Age { get; set; }
        [Required(ErrorMessage = "Gender is required")] //Zorunlu alan kuralı
        public string Gender { get; set; }
        [Required(ErrorMessage = "Country is required")] //Zorunlu alan kuralı
        public string Country { get; set; }

        //Dropdown (ülke seçimi) için seçenek listesi
        //başlangıçta boş bir liste atanır(null referans hatası almamak için)
        public IEnumerable<SelectListItem> CountryList { get; set; } = new List<SelectListItem>();
    }
}
