using BLL.Interfaces;
using DAL.DTOs.UsuarioDTOs;
using DAL.DTOs.UtilDTOs;
using Microsoft.AspNetCore.Http;

namespace API.ControllerService
{
    public class UsuarioControllerService : IUsuarioControllerService
    {
        private readonly IUsuarioService UsuarioService;

        public UsuarioControllerService(IUsuarioService usuarioService)
        {
            UsuarioService = usuarioService;
        }

        public async Task<IApiResult> Update(string id, UsuarioEdicionDTO usuarioEdicion)
        {
            bool existeUsuario = await UsuarioService.ExisteUsuarioById(id);
            
            if (!existeUsuario)
                return ApiResult<bool>.NotFound($"{id} no encontrado");

            if(existeUsuario && (id != usuarioEdicion.Id))
                return ApiResult<bool>.BadRequest();

            var resultUpdate = await UsuarioService.Update(usuarioEdicion);

            if(resultUpdate != null && !resultUpdate.OperacionCompletada)
                return ApiResult<bool>.Error(resultUpdate.Error);

            return ApiResult<object>.NoContent("Recurso actualizado correctamente");
        }
    }
}
