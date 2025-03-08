using DAL.Model;
using Microsoft.AspNetCore.Identity;

namespace Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<ResultadoOperacion<string>> Create(Usuario Usuario);
        //Task<ResultadoOperacion<int>> Delete(int id);
        Task<bool> ExisteUsuario(string email);
        Task<ResultadoOperacion<List<IdentityUser>>> GetAll();
        Task<ResultadoOperacion<IdentityUser>> GetById(string id);
        //Task<int> Update(IdentityUser Usuario);
    }
}