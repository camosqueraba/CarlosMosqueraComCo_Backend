using DAL.DTOs.UtilDTOs;
using DAL.Model;

namespace Repository.Interfaces
{
    public interface IPublicacionRepository
    {
        public Task<ResultadoOperacion<List<Publicacion>>> GetAll();
        public Task<ResultadoOperacion<Publicacion>> GetById(int id);        
        public Task<ResultadoOperacion<int>> Create(Publicacion publicacion);
        public Task<ResultadoOperacion<bool>> Update(Publicacion publicacion);
        public Task<ResultadoOperacion<int>> Delete(int id);
        public Task<bool> ExistePublicacion(int id);
    }
}