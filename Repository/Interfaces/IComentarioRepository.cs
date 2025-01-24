using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace Repository.Interfaces
{
    public interface IComentarioRepository
    {
        public Task<List<Comentario>> GetAll();

        public Task<Comentario> GetById(int id);

        public Task<int> Create(Comentario comentario);

        public Task<int> Update(Comentario comentario);

        public Task<int> Delete(int id);
    }
}