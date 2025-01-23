using AutoMapper;
using DAL.DTOs.PublicacionDTOs;
using DAL.Model.Publicacion;

namespace DAL.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PublicacionCreacionDTO, Publicacion>();
            CreateMap<PublicacionEdicionDTO, Publicacion>();
            CreateMap<Publicacion, PublicacionDTO>();

            /*
            CreateMap<CuentaCreacionDTO, Cuenta>();
            CreateMap<CuentaEdicionDTO, Cuenta>();
            CreateMap<Cuenta, CuentaDTO>();


            CreateMap<TransaccionCreacionDTO, Transaccion>();
            CreateMap<TransaccionCreacionTransferenciaDTO, Transaccion>();
            //CreateMap<TransaccionEdicionDTO, Transaccion>();
            CreateMap<Transaccion, TransaccionDTO>();
            */

        }
    }
}
