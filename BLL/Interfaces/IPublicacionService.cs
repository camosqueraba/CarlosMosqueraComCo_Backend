using DAL.DTOs.PublicacionDTOs;
using DAL.DTOs.UtilDTOs;

namespace BLL.Interfaces
{
    public interface IPublicacionService
    {
        public Task<ResultadoOperacion<List<PublicacionDTO>>> GetAll();

        public Task<ResultadoOperacion<PublicacionDetalleDTO>> GetById(int id);

        //public Task<PublicacionDTO> Create(PublicacionCreacionDTO publicacion);
        public Task<ResultadoOperacion<PublicacionDTO>> Create(PublicacionCreacionDTO publicacion); 
        public Task<PublicacionDTO> Update(PublicacionEdicionDTO publicacion);

        public Task<ResultadoOperacion<int>> Delete(int id);

        public Task<bool> ExistePublicacion(int id);
    }
}