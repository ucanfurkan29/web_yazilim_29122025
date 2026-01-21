using _13_automapper.Dto;
using _13_automapper.Models;
using AutoMapper;

namespace _13_automapper.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Urun, UrunDTO>();

            //eğer tam tersi de lazımsa şöyle yapabiliriz
            //CreateMap<UrunDTO, Urun>();
        }
    }
}
