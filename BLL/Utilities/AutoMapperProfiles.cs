using AutoMapper;
using DAL.DTOs.ComentarioDTOs;
using DAL.DTOs.PublicacionDTOs;
using DAL.DTOs.UsuarioDTOs;
using DAL.Model;
using Repository.IdentityEF;

namespace BLL.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PublicacionCreacionDTO, Publicacion>();
            CreateMap<PublicacionEdicionDTO, Publicacion>();
            CreateMap<Publicacion, PublicacionDTO>();
            CreateMap<Publicacion, PublicacionDetalleDTO>();

            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<ComentarioEdicionDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();
            CreateMap<ComentarioCreacionParaServiceDTO, Comentario>();

            CreateMap<UsuarioCreacionDTO, Usuario>();
            //CreateMap<ComentarioEdicionDTO, Comentario>();
            CreateMap<Usuario, UsuarioDTO>();

            CreateMap<CustomIdentityUser, UsuarioDTO>();
            CreateMap<CustomIdentityUser, UsuarioDetalleDTO>();
            CreateMap<UsuarioEdicionDTO, CustomIdentityUser>();
        }
    }
}