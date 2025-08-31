using DAL.DTOs.AutenticacionDTOs;
using DAL.DTOs.UsuarioDTOs;
using DAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repository.IdentityEF;
using Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository.Utils
{
    public class AutorizacionUtilsRepository : IAutorizacionUtilsRepository
    {
        public static UserManager<CustomIdentityUser> UserManager { get; set; }
        public static IConfiguration Configuration { get; set; }
        private  static SignInManager<CustomIdentityUser> _SignInManager { get; set; }

        public AutorizacionUtilsRepository(UserManager<CustomIdentityUser> userManager, SignInManager<CustomIdentityUser> signInManager, IConfiguration configuration)
        {
            UserManager = userManager;
            _SignInManager = signInManager;
            Configuration = configuration;
        }
        

        public  async Task<RespuestaAutenticacionDTO> ConstruirToken(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {

            var claims = new List<Claim>
            {
                new Claim("email", credencialesUsuarioDTO.Email)
            };

            var usuario = await UserManager.FindByEmailAsync(credencialesUsuarioDTO.Email);
            var claimsDB = await UserManager.GetClaimsAsync(usuario!);

            claims.AddRange(claimsDB);
            var llave = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["llavejwt"]!));
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

        
        public  async Task<bool> CheckPasswordIdentity(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            bool resultadoValidarPassword = false;

            CustomIdentityUser usuario = await UserManager.FindByEmailAsync(credencialesUsuarioDTO.Email);
            

            if(usuario == null)
            {
                return false;
            }

            SignInResult resultado = await _SignInManager.CheckPasswordSignInAsync(usuario, credencialesUsuarioDTO.Password!, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                resultadoValidarPassword = true;
            }

            return resultadoValidarPassword;
        }
        
    }
}
