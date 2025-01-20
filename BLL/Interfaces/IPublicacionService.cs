using DAL.DTOs.PublicacionDTOs;
using DAL.Model.Publicacion;

namespace BLL.Interfaces
{
    public interface IPublicacionService
    {
        public Task<List<Publicacion>> GetAll();

        public Task<Publicacion> GetById(int id);

        public Task<PublicacionDTO> Create(PublicacionCreacionDTO publicacion);

        public Task<Publicacion> Update(PublicacionEdicionDTO publicacion);

        public Task<int> Delete(int id);
    }
}