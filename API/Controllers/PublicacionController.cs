using API.ControllerService;
using API.Filtros;
using BLL.Interfaces;
using DAL.DTOs.PublicacionDTOs;
using DAL.DTOs.UsuarioDTOs;
using DAL.DTOs.UtilDTOs;
using DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/v1.0/publicaciones")]
    [ApiController]
    [Authorize]
    public class PublicacionController : ControllerBase
    {
        private readonly IPublicacionService PublicacionService;
        private readonly IPublicacionControllerService PublicacionControllerService;
        public PublicacionController(IPublicacionService publicacionService, IPublicacionControllerService publicacionControllerService)
        {
            PublicacionService = publicacionService;
            PublicacionControllerService = publicacionControllerService;
        }   
        
        [ServiceFilter(typeof(ApiResultFilter))]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<List<PublicacionDTO>>>> Get()
        {            
            return new ObjectResult(await PublicacionControllerService.Get());
        }


        [ServiceFilter(typeof(ApiResultFilter))]
        [HttpGet("{id}", Name = "ObtenerPublicacionPorId")]
        public async Task<ActionResult<ApiResponse<PublicacionDetalleDTO>>> Get(int id)
        {
            return new ObjectResult(await PublicacionControllerService.Get(id));
        }


        /// No se aplica [ServiceFilter(typeof(ApiResultFilter))] para poder generar ruta al recurso creado
        [HttpPost]
        public async Task<ActionResult<ApiResult<PublicacionDetalleDTO>>> Post([FromBody] PublicacionCreacionDTO publicacionCreacionDTO)
        {
            var resultCreate = await PublicacionControllerService.Create(publicacionCreacionDTO);
            
            if (resultCreate is ApiResult<PublicacionDTO> createdResult && createdResult.StatusCode == 201 && createdResult.Data != null)
            {
                return CreatedAtRoute("ObtenerPublicacionPorId",
                                        new { id = createdResult.Data.Id },
                                        createdResult.ToResponse());
            }
            
            return StatusCode(resultCreate.StatusCode, resultCreate.ToResponse());
        }

        [ServiceFilter(typeof(ApiResultFilter))]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Put(int id, [FromBody] PublicacionEdicionDTO publicacionEdicion)
        {
            return new ObjectResult(await PublicacionControllerService.Update(id, publicacionEdicion));
        }
        

        // DELETE api/<PublicacionesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            string titulo = "";
            int status_code = 0;

            bool existe = await PublicacionService.ExistePublicacion(id);
            
            if (!existe)
            {
                titulo = $"registro {id} no encontrado";
                status_code = 404;

                return NotFound(new ApiResponse<object>(false, status_code, titulo, false, null));
            }
                           
            ResultadoOperacion<int> resultado = await PublicacionService.Delete(id);

            if(resultado != null && resultado.OperacionCompletada)
            {
                titulo = "publicacion eliminada";
                status_code = 204;
            }

            return Ok(new ApiResponse<bool>(true, status_code, titulo, true,null));
        }
    }
}