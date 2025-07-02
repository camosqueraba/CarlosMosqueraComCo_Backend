using DAL.DTOs.UsuarioDTOs;
using DAL.DTOs.UtilDTOs;

namespace API.ControllerService
{
    public interface IUsuarioControllerService
    {
        public Task<IApiResult> Update(string id, UsuarioEdicionDTO usuarioEdicion);
    }
}
