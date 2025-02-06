using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs.ComentarioDTOs;
using DAL.Model;

namespace BLL.Interfaces
{
    public interface IComentarioService
    {
        public Task<List<ComentarioDTO>> GetAll();
        public Task<List<ComentarioDTO>> GetComentariosPorIdPublicacion(int idPublicacion);
        public Task<ComentarioDTO> GetById(int id);

        public Task<ComentarioDTO> Create(ComentarioCreacionParaServiceDTO comentario);

        public Task<ComentarioDTO> Update(ComentarioEdicionDTO comentario);

        public Task<int> Delete(int id);

        public Task<bool> ExisteComentario(int id);
    }
}
