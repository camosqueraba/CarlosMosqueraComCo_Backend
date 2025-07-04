using BLL.Interfaces;
using BLL.Services;
using DAL.DTOs.PublicacionDTOs;
using DAL.DTOs.UsuarioDTOs;
using DAL.DTOs.UtilDTOs;
using DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.ControllerService
{
    public class PublicacionControllerService : IPublicacionControllerService
    {
        private readonly IPublicacionService PublicacionService;
        public PublicacionControllerService(IPublicacionService publicacionService)
        {
            PublicacionService = publicacionService;
        }

        public  async Task<IApiResult> Get()
        {
            var resultGetAll = await PublicacionService.GetAll();

            
            if (resultGetAll != null && resultGetAll.DatosResultado != null && resultGetAll.DatosResultado.Count == 0)
                return ApiResult<object>.Ok(resultGetAll.DatosResultado, "Ningun recurso encontrado");
            /*
            if (existeUsuario && (id != usuarioEdicion.Id))
                return ApiResult<bool>.BadRequest();

            var resultUpdate = await PublicacionService.Update(usuarioEdicion);
            */

            if (resultGetAll != null && !resultGetAll.OperacionCompletada)
                return ApiResult<object>.Error(resultGetAll.Error);

            return ApiResult<object>.Ok(resultGetAll.DatosResultado, "Recursos encontrados");
        }

        public async Task<IApiResult> Get(int id)
        {
            var resultGetById = await PublicacionService.GetById(id);


            if (resultGetById != null && resultGetById.DatosResultado == null )
                return ApiResult<object>.NotFound("Ningun recurso encontrado");
            /*
            if (existeUsuario && (id != usuarioEdicion.Id))
                return ApiResult<bool>.BadRequest();

            var resultUpdate = await PublicacionService.Update(usuarioEdicion);
            */

            if (resultGetById != null && !resultGetById.OperacionCompletada)
                return ApiResult<object>.Error(resultGetById.Error);

            return ApiResult<object>.Ok(resultGetById.DatosResultado, "Recurso encontrado");
        }

        public async Task<IApiResult> Create(PublicacionCreacionDTO publicacionCreacionDTO)
        {
            var resultCreate = await PublicacionService.Create(publicacionCreacionDTO);


            if (resultCreate != null && resultCreate.DatosResultado == null)
                return ApiResult<object>.NotFound("Ningun recurso encontrado");
            /*
            if (existeUsuario && (id != usuarioEdicion.Id))
                return ApiResult<bool>.BadRequest();

            var resultUpdate = await PublicacionService.Update(usuarioEdicion);
            */

            if (resultCreate != null && !resultCreate.OperacionCompletada)
                return ApiResult<object>.Error(resultCreate.Error);

            //return CreatedAtRoute("ObtenetPublicacionPorId", new { id = publicacion.Id }, new ApiResponse<PublicacionDTO>(true, 201, "recurso creado", publicacion, null));

            return ApiResult<PublicacionDTO>.Created(resultCreate.DatosResultado, "Recurso creado");
        }

        public async Task<IApiResult> Update(int id, PublicacionEdicionDTO publicacionEdicion)
        {
            bool existePublicacion = await PublicacionService.ExistePublicacion(id);

            if (!existePublicacion)
                return ApiResult<bool>.NotFound($"{id} no encontrado");

            if (existePublicacion && (id != publicacionEdicion.Id))
                return ApiResult<bool>.BadRequest();

            var resultUpdate = await PublicacionService.Update(publicacionEdicion);

            if (resultUpdate != null && !resultUpdate.OperacionCompletada)
                return ApiResult<bool>.Error(resultUpdate.Error);

            return ApiResult<object>.OkWithoutData("Recurso actualizado correctamente");
        }
    }
}
