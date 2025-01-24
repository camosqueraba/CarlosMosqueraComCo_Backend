using API.Filtros;
using BLL.Interfaces;
using BLL.Services;
using DAL.DTOs.ComentarioDTOs;
using DAL.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/comentario")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioService ComentarioService;
        public ComentarioController(IComentarioService comentarioService)
        {
            ComentarioService = comentarioService;
        }

        // GET: api/<ComentarioesController>
        [HttpGet]
        public async Task<ActionResult<List<Comentario>>> Get()
        {
            List<Comentario> comentarioes = await ComentarioService.GetAll();

            string titulo = comentarioes.Count > 0 ? "registros encontrados" : "no se encontraron registros";

            return Ok(new ApiResponse<List<Comentario>>(true, 200, titulo, comentarioes, null));
        }


        // GET api/<ComentarioesController>/5
        [HttpGet("{id}", Name = "ObtenetComentarioPorId")]
        public async Task<ActionResult<ComentarioDTO>> Get(int id)
        {
            string titulo = "";
            int status_code = 0;

            ComentarioDTO comentario = await ComentarioService.GetById(id);

            if (comentario is null)
            {
                titulo = "registro no encontrado";
                status_code = 404;

                return NotFound(new ApiResponse<ComentarioDTO>(true, status_code, titulo, null, null));
            }

            return Ok(new ApiResponse<ComentarioDTO>(true, 200, titulo, comentario, null));
        }


        // POST api/<ComentarioesController>
        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<ActionResult<ApiResponse<ComentarioDTO>>> Post([FromBody] ComentarioCreacionDTO comentarioCreacionDTO)
        {
            ComentarioDTO comentario = null;

            if (ModelState.IsValid)
            {

                comentario = await ComentarioService.Create(comentarioCreacionDTO);
            }

            if (comentario == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("ObtenetComentarioPorId", new { id = comentario.Id }, new ApiResponse<ComentarioDTO>(true, 201, "recurso creado", comentario, null));
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

                return NotFound(new ApiResponse<Comentario>(true, status_code, titulo, null, null));
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

                return NotFound(new ApiResponse<Comentario>(true, status_code, titulo, null, null));
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
