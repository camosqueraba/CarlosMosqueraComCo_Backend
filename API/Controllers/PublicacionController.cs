using API.Filtros;
using BLL.Interfaces;
using DAL.DTOs.PublicacionDTOs;
using DAL.Model;
using DAL.Model.Publicacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/publicacion")]
    [ApiController]
    public class PublicacionController : ControllerBase
    {
        private readonly IPublicacionService PublicacionService; 
        public PublicacionController(IPublicacionService publicacionService)
        {
            PublicacionService = publicacionService;
        }

        // GET: api/<PublicacionesController>
        [HttpGet]
        public async Task<ActionResult<List<Publicacion>>> Get()
        {
            List<Publicacion> publicaciones = await PublicacionService.GetAll();

            string titulo = "registros encontrados";

            if (publicaciones.Count == 0)
                titulo = "no se encontraron registros";

            return Ok(new ApiResponse<List<Publicacion>>(true, 200, titulo, publicaciones, null));
        }
    

        // GET api/<PublicacionesController>/5
        [HttpGet("{id}", Name = "ObtenetPublicacionPorId")]
        public string Get(int id)
        {
            return "value";
        }

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

        // PUT api/<PublicacionesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PublicacionesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}