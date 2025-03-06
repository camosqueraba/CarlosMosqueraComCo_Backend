using DAL.DTOs.UsuarioDTOs;
using DAL.Model;

namespace BLL.Interfaces
{
    public interface IUsuarioService
    {
        public Task<ResultadoOperacion<UsuarioDTO>> Create(UsuarioCreacionDTO usuarioCreaconDTO);
        //Task<ResultadoOperacion<int>> Delete(int id);
        public Task<bool> ExisteUsuario(string email);
        public Task<List<UsuarioDTO>> GetAll();
        //Task<ResultadoOperacion<IdentityUser>> GetById(int id);
        //Task<int> Update(IdentityUser Usuario);
    }
}
