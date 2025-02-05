using DAL.DTOs.PublicacionDTOs;
using DAL.Model;

namespace BLL.Interfaces
{
    public interface IPublicacionService
    {
        public Task<List<PublicacionDTO>> GetAll();

        public Task<PublicacionDetalleDTO> GetById(int id);

        //public Task<PublicacionDTO> Create(PublicacionCreacionDTO publicacion);
        public Task<ResultadoOperacion<PublicacionDTO>> Create(PublicacionCreacionDTO publicacion); 
        public Task<PublicacionDTO> Update(PublicacionEdicionDTO publicacion);

        public Task<int> Delete(int id);

        public Task<bool> ExistePublicacion(int id);
    }
}