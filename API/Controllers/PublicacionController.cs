using API.Filtros;
using BLL.Interfaces;
using DAL.DTOs.PublicacionDTOs;
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
        public PublicacionController(IPublicacionService publicacionService)
        {
            PublicacionService = publicacionService;
        }

        // GET: api/<PublicacionesController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<PublicacionDTO>>> Get()
        { 
            List<PublicacionDTO> publicaciones = await PublicacionService.GetAll();

            string titulo = publicaciones.Count > 0 ? "registros encontrados" : "no se encontraron registros";

            return Ok(new ApiResponse<List<PublicacionDTO>>(true, 200, titulo, publicaciones, null));
        }
    

        // GET api/<PublicacionesController>/5
        [HttpGet("{id}", Name = "ObtenetPublicacionPorId")]
        public async Task<ActionResult<PublicacionDetalleDTO>> Get(int id)
        {
            string titulo = "";
            int status_code = 0;
            ResultadoOperacion<PublicacionDetalleDTO> resultadoOperacion = null;
            
            resultadoOperacion  = await PublicacionService.GetById(id);
            PublicacionDetalleDTO publicacion = resultadoOperacion.DatosResultado;

            if (publicacion is null)
            {
                titulo = "registro no encontrado";
                status_code = 404;

                return NotFound(new ApiResponse<PublicacionDetalleDTO>(true, status_code, titulo, null, null));
            }            

            return Ok(new ApiResponse<PublicacionDetalleDTO>(true, 200, titulo, publicacion, null));
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

                return NotFound(new ApiResponse<bool>(false, status_code, titulo, false, null));
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