using _13_automapper.Dto;
using _13_automapper.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _13_automapper.Controllers
{
    public class UrunController : Controller
    {
        private readonly IMapper _mapper; // AutoMapper arayüzü için alan
        public UrunController(IMapper mapper)//program.cs den AddAutoMapper ile eklenen IMapper örneğini alır
        {
            _mapper = mapper; // Yapılandırıcı enjeksiyonu ile IMapper örneğini al
        }
        public IActionResult Index()
        {
            Urun urunEntity = new Urun
            {
                Id = 1,
                Ad = "Laptop",
                Fiyat = 7500.00m,
                EklenmeTarihi = DateTime.Now
            };
            //Dönüştürme işlemi
            //urunEntity nesnesini UrunDTO nesnesine dönüştür
            var urunDto = _mapper.Map<UrunDTO>(urunEntity); // Urun nesnesini UrunDto'ya dönüştür

            //urunDto.Ad -> "Laptop" olarak geldi.
            //urunDto.Fiyat -> 7500.00 olarak geldi.
            //Id ve EklenmeTarihi alanları UrunDTO'da olmadığı için gelmedi.

            return View(urunDto);
        }
    }
}
