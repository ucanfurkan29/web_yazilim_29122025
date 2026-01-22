using _14_dependency_injection.Models;
using _14_dependency_injection.Services.Abstract;

namespace _14_dependency_injection.Services.Concrete
{
    public class OgrenciManager : IOgrenciService
    {
        public List<Ogrenci> ListeyiGetir()
        {
            return new List<Ogrenci>
            {
                new Ogrenci { Id = 1, Ad = "Ahmet", Bolum = "Bilgisayar Mühendisliği" },
                new Ogrenci { Id = 2, Ad = "Ayşe", Bolum = "Elektrik-Elektronik Mühendisliği" },
                new Ogrenci { Id = 3, Ad = "Mehmet", Bolum = "Makine Mühendisliği" }
            };
        }
    }
}
