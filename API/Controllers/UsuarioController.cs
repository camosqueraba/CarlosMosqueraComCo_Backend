using API.Filtros;
using BLL.Interfaces;
using BLL.Services;
using DAL.DTOs.UsuarioDTOs;
using DAL.DTOs.UsuarioDTOs;
using DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1.0/usuarios")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IUsuarioService UsuarioService;
        private readonly IConfiguration configuration;        

        public UsuarioController(IUsuarioService usuarioService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager ,IConfiguration configuration)
        {
            UsuarioService = usuarioService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("registro-usuario")]
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Registrar(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            var usuario = new IdentityUser
            {
                UserName = credencialesUsuarioDTO.Email,
                Email = credencialesUsuarioDTO.Email
            };

            var resultado = await userManager.CreateAsync(usuario, credencialesUsuarioDTO.Password);

            if (resultado.Succeeded)
            {
                var respuestaAutenticacion = await ConstruirToken(credencialesUsuarioDTO);
                return respuestaAutenticacion;
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return ValidationProblem();
            }
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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Login(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            var usuario = await userManager.FindByEmailAsync(credencialesUsuarioDTO.Email);
            
            if (usuario is null)
            {
                return RetornarLoginIncorrecto();
            }

            var resultado = await signInManager.CheckPasswordSignInAsync(usuario, credencialesUsuarioDTO.Password!, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuarioDTO);
            }
            else
            {
                return RetornarLoginIncorrecto();
            }
        }

        private ActionResult RetornarLoginIncorrecto()
        {
            ModelState.AddModelError(string.Empty, "Login incorrecto");
            return ValidationProblem();
        }

        private async Task<RespuestaAutenticacionDTO> ConstruirToken(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {           

            var claims = new List<Claim>
            {
                new Claim("email", credencialesUsuarioDTO.Email)
            };

            var usuario = await userManager.FindByEmailAsync(credencialesUsuarioDTO.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario!);

            claims.AddRange(claimsDB);
            var llave = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]!));
            var credenciales = new Microsoft.IdentityModel.Tokens.SigningCredentials(llave, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var tokenDeSeguridad = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: credenciales);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDeSeguridad);

            return new RespuestaAutenticacionDTO
            {
                Token = token,
                Expiracion = expiracion
            };

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
        // PUT api/<PublicacionesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<PublicacionDTO>>> Put(int id, [FromBody] PublicacionEdicionDTO usuarioEdicion)
        {
            string titulo = "";
            int status_code = 0;

            PublicacionDetalleDTO usuarioActual;
            ResultadoOperacion<PublicacionDetalleDTO> resultadoOperacion = await PublicacionService.GetById(id);
            usuarioActual = resultadoOperacion.DatosResultado;
            PublicacionDTO usuarioEditada;

            if (usuarioActual is null)
            {
                titulo = "registro no encontrado";
                status_code = 404;

                return NotFound(new ApiResponse<Publicacion>(false, status_code, titulo, null, null));
            }
            else
            {
                usuarioEdicion.Id = usuarioActual.Id;
                usuarioEdicion.FechaCreacion = usuarioActual.FechaCreacion;

                usuarioEditada = await PublicacionService.Update(usuarioEdicion);
                titulo = "usuario editada";
                status_code = 200;
            }

            return Ok(new ApiResponse<PublicacionDTO>(true, status_code, titulo, usuarioEditada, null));
        }
        */

        /*
        // DELETE api/<PublicacionesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse_>> Delete(int id)
        {
            string titulo = "";
            int status_code = 0;

            bool existe = await PublicacionService.ExistePublicacion(id);

            if (!existe)
            {
                titulo = $"registro {id} no encontrado";
                status_code = 404;

                return NotFound(new ApiResponse_(false, status_code, titulo, null));
            }

            ResultadoOperacion<int> resultado = await PublicacionService.Delete(id);

            if (resultado != null && resultado.OperacionCompletada)
            {
                titulo = "usuario eliminada";
                status_code = 204;
            }

            return Ok(new ApiResponse_(true, status_code, titulo, null));
        }
        */
    }
}