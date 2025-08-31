using DAL.DTOs.PublicacionDTOs;
using DAL.DTOs.UtilDTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.ControllerService
{
    public interface IPublicacionControllerService
    {
        public Task<IApiResult> Get();
        public Task<IApiResult> Get(int id);
        public Task<IApiResult> Create(PublicacionCreacionDTO publicacionCreacionDTO);
        public Task<IApiResult> Update(int id, PublicacionEdicionDTO publicacionEdicion);
    }
}
