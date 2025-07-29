using AutoMapper;
using Models.DTOs;
using Models.Entities;

namespace Implementation.Utilitys
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Producto, ProductoDTO>().ReverseMap();
        }
    }
}
