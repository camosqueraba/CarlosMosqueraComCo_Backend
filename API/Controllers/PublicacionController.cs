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

        //[ServiceFilter(typeof(ApiResultFilter))]
        [HttpPost]
        public async Task<ActionResult<ApiResult<PublicacionDetalleDTO>>> Post([FromBody] PublicacionCreacionDTO publicacionCreacionDTO)
        {
            var resultCreate = await PublicacionControllerService.Create(publicacionCreacionDTO);
            //if (resultCreate != null && resultCreate.StatusCode == 201)
            if (resultCreate is ApiResult<PublicacionDTO> createdResult && createdResult.StatusCode == 201 && createdResult.Data != null)
            {
                return CreatedAtRoute("ObtenerPublicacionPorId",
                                        new { id = createdResult.Data.Id },
                                        createdResult.ToResponse());
            }
            //return CreatedAtRoute("ObtenetPublicacionPorId", new { id = publicacion.Id }, new ApiResponse<PublicacionDTO>(true, 201, "recurso creado", publicacion, null));
            return StatusCode(resultCreate.StatusCode, resultCreate.ToResponse());
            //return new ObjectResult(await PublicacionControllerService.Create(publicacionCreacionDTO));
        }

        /*
        // POST api/<PublicacionesController>        
        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<ActionResult<ApiResponse<PublicacionDTO>>> Post([FromBody] PublicacionCreacionDTO publicacionCreacionDTO)
        {
            PublicacionDTO publicacion = null;
            
            if (ModelState.IsValid)
            {
                publicacion = await PublicacionService.Create(publicacionCreacionDTO);                
            }  

            if (publicacion == null)
            {
                return BadRequest();
            }
            
            return CreatedAtRoute("ObtenetPublicacionPorId", new { id = publicacion.Id }, new ApiResponse<PublicacionDTO>(true, 201, "recurso creado", publicacion, null));
        }
        */

        /*
        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<ActionResult<ApiResponse<PublicacionDTO>>> Post([FromBody] PublicacionCreacionDTO publicacionCreacionDTO)
        {
            PublicacionDTO publicacion = null;
            ResultadoOperacion<PublicacionDTO> resultadoOperacionService = null;
            ApiResponse<PublicacionDTO> apiResponse;
            
            if (ModelState.IsValid)
            {
                resultadoOperacionService = await PublicacionService.Create(publicacionCreacionDTO);
            }

            if (resultadoOperacionService != null && resultadoOperacionService.OperacionCompletada == true)
            {
                publicacion = resultadoOperacionService.DatosResultado;                
            }

            if (resultadoOperacionService != null && resultadoOperacionService.OperacionCompletada == false)
            {
                List<string> errores = new List<string>();
                string error_origen = $"{resultadoOperacionService.Origen} : {resultadoOperacionService.Error}";
                errores.Add(error_origen);
                apiResponse = new ApiResponse<PublicacionDTO>(false, 400, "error creando recurso", null, errores);
                return BadRequest(apiResponse);
            }

            return CreatedAtRoute("ObtenetPublicacionPorId", new { id = publicacion.Id }, new ApiResponse<PublicacionDTO>(true, 201, "recurso creado", publicacion, null));
        }
        */

        // PUT api/<PublicacionesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<PublicacionDTO>>> Put(int id, [FromBody] PublicacionEdicionDTO publicacionEdicion)
        {
            string titulo = "";
            int status_code = 0;

            PublicacionDetalleDTO publicacionActual;
            ResultadoOperacion<PublicacionDetalleDTO> resultadoOperacion = await PublicacionService.GetById(id);
            publicacionActual = resultadoOperacion.DatosResultado;
            PublicacionDTO publicacionEditada;

            if (publicacionActual is null)
            {
                titulo = "registro no encontrado";
                status_code = 404;

                return NotFound(new ApiResponse<Publicacion>(false, status_code, titulo, null, null));
            }
            else
            {
                publicacionEdicion.Id = publicacionActual.Id;
                publicacionEdicion.FechaCreacion = publicacionActual.FechaCreacion;
                
                publicacionEditada = await PublicacionService.Update(publicacionEdicion);
                titulo = "publicacion editada";
                status_code = 200;
            }

            return Ok(new ApiResponse<PublicacionDTO>(true, status_code, titulo, publicacionEditada, null));
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