using AutoMapper;
using MyVilla_API.Models;
using MyVilla_API.Models.DTO;

namespace MyVilla_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>().ReverseMap();

            CreateMap<Villa, VillaCreateDTO>().ReverseMap();

            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
        }

    }
}
