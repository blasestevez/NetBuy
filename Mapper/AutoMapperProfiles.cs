using AutoMapper;
using LaChozaComercial.Models;
using LaChozaComercial.Models.DTOs;

namespace LaChozaComercial.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Publicacion, PublicacionDTO>()
                .ForMember(p => p.vendedorNombre, opt => opt.MapFrom(p => p.autorPublicacion.UserName))
                .ReverseMap();

            CreateMap<Publicacion, CreatePublicacionRequestDTO>().ReverseMap();
        }
    }
}
