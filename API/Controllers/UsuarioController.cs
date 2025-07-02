using API.ControllerService;
using API.Filtros;
using BLL.Interfaces;
using DAL.DTOs.AutenticacionDTOs;
using DAL.DTOs.UsuarioDTOs;
using DAL.DTOs.UtilDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1.0/usuarios")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        
        private readonly IAutorizacionUtilsService autorizacionUtilsService;
        private readonly IUsuarioService UsuarioService;
        private readonly IUsuarioControllerService UsuarioControllerService;
        private readonly IConfiguration configuration;        

        public UsuarioController(IUsuarioService usuarioService, IUsuarioControllerService usuarioControllerService,IAutorizacionUtilsService autorizacionUtilsService, IConfiguration configuration)
        {
            UsuarioService = usuarioService;
            UsuarioControllerService = usuarioControllerService;
            this.autorizacionUtilsService = autorizacionUtilsService;
            this.configuration = configuration;
        }

        [HttpPost("registro")]
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Registrar(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            ResultadoOperacion<RespuestaAutenticacionDTO> resultado = await UsuarioService.RegistrarUsuario(credencialesUsuarioDTO);
            string titulo;
            RespuestaAutenticacionDTO resultadoRegistro = new();
            List<string> errores = new List<string>();
            bool operacionCompletada; 

            if (resultado != null && resultado.OperacionCompletada)
            {
                titulo = "registro completado";
                resultadoRegistro = resultado.DatosResultado;
                operacionCompletada = true;
            }
            else if (resultado != null && !resultado.OperacionCompletada)
            {
                titulo = "registro NO completado";
                errores = resultado.Error.Split("|").ToList();
                operacionCompletada = false;
            }
            else
            {
                titulo = "error al registrar";
                operacionCompletada = false;
            }
            
            return Ok(new ApiResponse<RespuestaAutenticacionDTO>(operacionCompletada, 200, titulo, resultadoRegistro, errores));
            
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<ActionResult<ApiResponse<UsuarioDTO>>> Post([FromBody] UsuarioCreacionDTO usuarioCreacionDTO)
        {
            UsuarioDTO usuario = null;
            ResultadoOperacion<UsuarioDTO> resultadoOperacionService = null;
            ApiResponse<UsuarioDTO> apiResponse;

            if (ModelState.IsValid)
            {
                resultadoOperacionService = await UsuarioService.Create(usuarioCreacionDTO);
            }

            if (resultadoOperacionService != null && resultadoOperacionService.OperacionCompletada == true)
            {
                usuario = resultadoOperacionService.DatosResultado;
            }

            if (resultadoOperacionService != null && resultadoOperacionService.OperacionCompletada == false)
            {
                List<string> errores = [];
                string error_origen = $"{resultadoOperacionService.Origen} : {resultadoOperacionService.Error}";
                errores.Add(error_origen);
                apiResponse = new ApiResponse<UsuarioDTO>(false, 400, "error creando recurso", null, errores);
                return BadRequest(apiResponse);
            }

            return CreatedAtRoute("ObtenetUsuarioPorId", new { id = usuario.Id }, new ApiResponse<UsuarioDTO>(true, 201, "recurso creado", usuario, null));
        }               
       
        
          // GET: api/<UsuarioesController>
        [HttpGet]
        
        public async Task<ActionResult<ApiResponse<List<UsuarioDTO>>>> Get()
        {
            string titulo = "registros encontrados";
            List<string> errores = [];
            List<UsuarioDTO> usuarios = new();

            ResultadoOperacion<List<UsuarioDTO>> resultadoOperacion = await UsuarioService.GetAll();
            
            if(resultadoOperacion != null && resultadoOperacion.OperacionCompletada)
            {
                if(resultadoOperacion.DatosResultado != null && resultadoOperacion.DatosResultado.Count < 1)
                {
                    titulo = "no se encontraron registros";
                }
                else
                {
                    usuarios = resultadoOperacion.DatosResultado;
                }
            }
            else
            {
                titulo = "error en la consulta";
                errores.Add(resultadoOperacion.Error);
                errores.Add(resultadoOperacion.Origen);
            }
                //List<UsuarioDTO> usuarioes =

                //string titulo = usuarioes.Count > 0 ? "registros encontrados" : "no se encontraron registros";

            return Ok(new ApiResponse<List<UsuarioDTO>>(true, 200, titulo, usuarios, errores));
        }

        
         //GET api/<PublicacionesController>/5
        [HttpGet("{id}", Name = "ObtenetUsuarioPorId")]
        public async Task<ActionResult<UsuarioDetalleDTO>> Get(string id)
        {
            string titulo = "";
            int status_code = 0;
            ResultadoOperacion<UsuarioDetalleDTO> resultadoOperacion = null;
            
            resultadoOperacion  = await UsuarioService.GetById(id);
            UsuarioDetalleDTO usuario = resultadoOperacion.DatosResultado;

            if (usuario is null)
            {
                titulo = "registro no encontrado";
                status_code = 404;

                return NotFound(new ApiResponse<UsuarioDetalleDTO>(true, status_code, titulo, null, null));
            }            

            return Ok(new ApiResponse<UsuarioDetalleDTO>(true, 200, titulo, usuario, null));
        }

        /*
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Put(string id, [FromBody] UsuarioEdicionDTO usuarioEdicion)
        {
            string titulo = "";
            int status_code = 0;

            UsuarioDetalleDTO usuarioActual;
            ResultadoOperacion<UsuarioDTO> resultadoOperacionEdicion;

            ResultadoOperacion<UsuarioDetalleDTO> resultadoOperacion = await UsuarioService.GetById(id);
            usuarioActual = resultadoOperacion.DatosResultado;
            UsuarioDTO usuarioEditada;

            if (usuarioActual is null)
            {
                titulo = "registro no encontrado";
                status_code = 404;

                return NotFound(new ApiResponse<UsuarioDTO>(false, status_code, titulo, null, null));
            }
            else
            {
                usuarioEdicion.Id = usuarioActual.Id;
                usuarioEdicion.FechaCreacion = usuarioActual.FechaCreacion;

                resultadoOperacionEdicion = await UsuarioService.Update(usuarioEdicion);
                titulo = "usuario editada";
                status_code = 200;
            }

            //return Ok(new ApiResponse<UsuarioDTO>(true, status_code, titulo, usuarioEditada, null));
            return new ObjectResult(new ApiResponse<UsuarioDTO>(true, status_code, titulo, resultadoOperacionEdicion.DatosResultado, null));
        }
        */

        [ServiceFilter(typeof(ApiResultFilter))]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Put(string id, [FromBody] UsuarioEdicionDTO usuarioEdicion)
        {
            return new ObjectResult(await UsuarioControllerService.Update(id, usuarioEdicion));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(string id)
        {
            string titulo = "";
            int status_code = 0;

            bool existe = await UsuarioService.ExisteUsuarioById(id);

            if (!existe)
            {
                titulo = $"registro {id} no encontrado";
                status_code = 404;

                return NotFound(new ApiResponse<bool>(false, status_code, titulo, false, null));
            }

            ResultadoOperacion<int> resultado = await UsuarioService.Delete(id);

            if (resultado != null && resultado.OperacionCompletada)
            {
                titulo = "usuario eliminada";
                status_code = 204;
            }

            return Ok(new ApiResponse<bool>(true, status_code, titulo, true, null));
        }

    }
}