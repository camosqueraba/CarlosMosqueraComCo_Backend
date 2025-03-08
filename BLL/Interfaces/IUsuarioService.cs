using DAL.DTOs.UsuarioDTOs;
using DAL.Model;

namespace BLL.Interfaces
{
    public interface IUsuarioService
    {
        public Task<ResultadoOperacion<UsuarioDTO>> Create(UsuarioCreacionDTO usuarioCreaconDTO);
        //Task<ResultadoOperacion<int>> Delete(int id);
        public Task<bool> ExisteUsuario(string email);
        public Task<ResultadoOperacion<List<UsuarioDTO>>> GetAll();
        Task<ResultadoOperacion<UsuarioDetalleDTO>> GetById(string id);
        //Task<int> Update(IdentityUser Usuario);
    }
}
