using AutoMapper;
using LaChozaComercial.Models;
using LaChozaComercial.Models.DTOs;
using System.Net.Mail;

namespace LaChozaComercial.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Mapeo de publicaciones
            CreateMap<Publicacion, PublicacionDTO>()
                .ForMember(p => p.vendedorNombre, opt => opt.MapFrom(p => p.autorPublicacion.UserName))
                .ReverseMap();

            CreateMap<Publicacion, CreatePublicacionRequestDTO>().ReverseMap();

            // Mapeo de usuarios

            CreateMap<UsuarioDTO, Usuario>().ReverseMap();

            CreateMap<CreateUsuarioRequestDTO, Usuario>()
                .ForMember(u => u.PasswordHash, opt => opt.MapFrom(u => u.password))
                .ReverseMap();


            
        }
    }
}
