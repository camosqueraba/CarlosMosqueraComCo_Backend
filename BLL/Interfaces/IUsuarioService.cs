using DAL.DTOs.AutenticacionDTOs;
using DAL.DTOs.UsuarioDTOs;
using DAL.DTOs.UtilDTOs;

namespace BLL.Interfaces
{
    public interface IUsuarioService
    {
        public Task<ResultadoOperacion<UsuarioDTO>> Create(UsuarioCreacionDTO usuarioCreaconDTO);
        public Task<ResultadoOperacion<int>> Delete(string id);
        public Task<bool> ExisteUsuario(string email);
        public Task<bool> ExisteUsuarioById(string id);
        public Task<ResultadoOperacion<List<UsuarioDTO>>> GetAll();
        public Task<ResultadoOperacion<UsuarioDetalleDTO>> GetById(string id);
        public Task<ResultadoOperacion<RespuestaAutenticacionDTO>> RegistrarUsuario(CredencialesUsuarioDTO credencialesUsuarioDTO);
        public Task<ResultadoOperacion<bool>> Update(UsuarioEdicionDTO Usuario);
    }
}
