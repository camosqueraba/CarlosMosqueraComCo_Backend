using DAL.DTOs.UsuarioDTOs;
using DAL.Model;
using Microsoft.AspNetCore.Identity;

namespace Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<ResultadoOperacion<int>> Create(Usuario Usuario);
        //Task<ResultadoOperacion<int>> Delete(int id);
        Task<bool> ExisteUsuario(string email);
        Task<List<Usuario>> GetAll();
        Task<ResultadoOperacion<UsuarioDetalleDTO>> GetById(string id);
        //Task<int> Update(IdentityUser Usuario);
    }
}