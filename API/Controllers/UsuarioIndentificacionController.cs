using BLL.Interfaces;
using DAL.DTOs.AutenticacionDTOs;
using DAL.DTOs.UsuarioDTOs;
using DAL.DTOs.UtilDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{    
    [Route("api/v1.0/identificacion")]
    [ApiController]
    public class UsuarioIndentificacionController : ControllerBase
    {        
        private readonly IAutorizacionUtilsService AutorizacionUtilsService;
        private readonly IUsuarioService UsuarioService;
        private readonly IConfiguration Configuration;

        public UsuarioIndentificacionController(IUsuarioService usuarioService, IAutorizacionUtilsService autorizacionUtilsService, IConfiguration configuration)
        {
            UsuarioService = usuarioService;
            //this.userManager = userManager;
            //this.signInManager = signInManager;
            this.AutorizacionUtilsService = autorizacionUtilsService;
            this.Configuration = configuration;
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

        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Login(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            RespuestaAutenticacionDTO algo = await AutorizacionUtilsService.LoginAutorizacionUtilsService(credencialesUsuarioDTO);

            return Ok(new ApiResponse<RespuestaAutenticacionDTO>(algo.AutenticacionCorrecta,200 ,algo.MensajeResultado, algo, null));
            
        }

        [HttpGet("saludo")]
        [AllowAnonymous]
        public ActionResult Get()
        {
            return Ok("Saludos");
        }
    }
}
