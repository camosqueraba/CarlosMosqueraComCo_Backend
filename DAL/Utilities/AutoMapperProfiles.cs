using AutoMapper;
using DAL.DTOs.ComentarioDTOs;
using DAL.DTOs.PublicacionDTOs;
using DAL.Model;

namespace DAL.Utilities
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
        }
    }
}