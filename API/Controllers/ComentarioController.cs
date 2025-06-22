using API.Filtros;
using BLL.Interfaces;
using BLL.Services;
using DAL.DTOs.ComentarioDTOs;
using DAL.DTOs.UtilDTOs;
using DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{    
    [ApiController]
    [Route("api/v1.0/publicaciones/{idPublicacion:int}/comentarios")]
    [Authorize]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioService ComentarioService;
        private readonly IPublicacionService PublicacionService;
        public ComentarioController(IComentarioService comentarioService, IPublicacionService publicacionService)
        {
            ComentarioService = comentarioService;
            PublicacionService = publicacionService;
        }

        // GET: api/<ComentarioesController>
        //[HttpGet]
        //public async Task<ActionResult<List<Comentario>>> Get()
        //{
        //    List<ComentarioDTO> comentarios = await ComentarioService.GetAll();

        //    string titulo = comentarios.Count > 0 ? "registros encontrados" : "no se encontraron registros";

        //    return Ok(new ApiResponse<List<ComentarioDTO>>(true, 200, titulo, comentarios, null));
        //}

        [HttpGet]
        public async Task<ActionResult<List<Comentario>>> Get(int idPublicacion)
        {
            List<ComentarioDTO> comentarios = [];
            bool existePublicacion = await PublicacionService.ExistePublicacion(idPublicacion);

            if (existePublicacion)
            {
                comentarios = await ComentarioService.GetComentariosPorIdPublicacion(idPublicacion);
            }
            else
            {
                return BadRequest(new ApiResponse<List<ComentarioDTO>>(false, 400, "publicacion no existe", comentarios, null));
            }
            
            string titulo = comentarios.Count > 0 ? "registros encontrados" : "no se encontraron registros";

            return Ok(new ApiResponse<List<ComentarioDTO>>(true, 200, titulo, comentarios, null));
        }


        // GET api/<ComentarioesController>/5
        [HttpGet("{idComentario}", Name = "ObtenerComentarioPorId")]
        public async Task<ActionResult<ComentarioDTO>> GetById([FromRoute] int idPublicacion, [FromRoute]int idComentario )
        {
            string titulo = "";
            int status_code = 0;

            ComentarioDTO comentario = null;

            bool existePublicacion = await PublicacionService.ExistePublicacion(idPublicacion);

            if (existePublicacion)
            {
                comentario = await ComentarioService.GetById(idComentario);
            }
            else
            {
                return BadRequest(new ApiResponse<ComentarioDTO>(false, 400, "publicacion no existe", null, null));
            }

            if (comentario is null)
            {
                titulo = "registro no encontrado";
                status_code = 404;

                return NotFound(new ApiResponse<ComentarioDTO>(false, status_code, titulo, null, null));
            }

            return Ok(new ApiResponse<ComentarioDTO>(true, 200, titulo, comentario, null));
        }


        // POST api/<ComentarioesController>
        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<ActionResult<ApiResponse<ComentarioDTO>>> Post([FromRoute]int idPublicacion, [FromBody] ComentarioCreacionDTO comentarioCreacionDTO)
        {
            ComentarioDTO comentario = null;
            ComentarioCreacionParaServiceDTO comentarioCreacionParaServiceDTO = new();

            if (ModelState.IsValid)
            {
                bool existePublicacion = await PublicacionService.ExistePublicacion(idPublicacion);

                if (existePublicacion)
                {
                    comentarioCreacionParaServiceDTO.PublicacionId = idPublicacion;
                    comentarioCreacionParaServiceDTO.Contenido = comentarioCreacionDTO.Contenido;
                    comentario = await ComentarioService.Create(comentarioCreacionParaServiceDTO);
                }
                else
                {
                    return BadRequest(new ApiResponse<ComentarioDTO>(false, 400, "publicacion no existe", comentario, null));
                }
                
            }

            if (comentario == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("ObtenerComentarioPorId", new { idPublicacion, idComentario = comentario.Id  }, new ApiResponse<ComentarioDTO>(true, 201, "recurso creado", comentario, null));
        }


        // PUT api/<ComentarioesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ComentarioDTO>>> Put(int id, [FromBody] ComentarioEdicionDTO comentarioEdicion)
        {
            string titulo = "";
            int status_code = 0;

            ComentarioDTO comentarioActual = await ComentarioService.GetById(id);
            ComentarioDTO comentarioEditada;

            if (comentarioActual is null)
            {
                titulo = "registro no encontrado";
                status_code = 404;

                return NotFound(new ApiResponse<Comentario>(false, status_code, titulo, null, null));
            }
            else
            {
                comentarioEdicion.Id = comentarioActual.Id;
                comentarioEdicion.FechaCreacion = comentarioActual.FechaCreacion;

                comentarioEditada = await ComentarioService.Update(comentarioEdicion);
                titulo = "comentario editada";
                status_code = 200;
            }

            return Ok(new ApiResponse<ComentarioDTO>(true, status_code, titulo, comentarioEditada, null));
        }


        // DELETE api/<ComentarioesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<ComentarioDTO>>> Delete(int id)
        {
            string titulo = "";
            int status_code = 0;

            ComentarioDTO comentarioActual = await ComentarioService.GetById(id);

            if (comentarioActual is null)
            {
                titulo = "registro no encontrado";
                status_code = 404;

                return NotFound(new ApiResponse<Comentario>(false, status_code, titulo, null, null));
            }
            else
            {
                int result = await ComentarioService.Delete(id);
                if (result > 0)
                {
                    titulo = "comentario eliminada";
                    status_code = 204;
                }

            }

            return Ok(new ApiResponse<ComentarioDTO>(true, status_code, titulo, null, null));
        }
    }
}
