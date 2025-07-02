using DAL.DTOs.AutenticacionDTOs;
using DAL.DTOs.UsuarioDTOs;
using DAL.DTOs.UtilDTOs;
using DAL.Model;
using Microsoft.AspNetCore.Identity;
using Repository.IdentityEF;

namespace Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<ResultadoOperacion<string>> Create(Usuario Usuario);
        Task<ResultadoOperacion<int>> Delete(string id);
        Task<bool> ExisteUsuario(string email);
        Task<bool> ExisteUsuarioById(string id);
        Task<ResultadoOperacion<List<CustomIdentityUser>>> GetAll();
        Task<ResultadoOperacion<CustomIdentityUser>> GetById(string id);
        //Task<ResultadoOperacion<CustomIdentityUser>> GetById(string id);

        Task<ResultadoOperacion<RespuestaAutenticacionDTO>> RegistrarUsuario(CredencialesUsuarioDTO credencialesUsuarioDTO);
        Task<ResultadoOperacion<int>> Update(CustomIdentityUser Usuario);
    }
}