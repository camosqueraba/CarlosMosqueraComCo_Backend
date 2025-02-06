using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using DAL.DTOs.ComentarioDTOs;
using DAL.Model;
using Repository.Interfaces;
using Repository.Repositories;

namespace BLL.Services
{
    public class ComentarioService : IComentarioService
    {
        private readonly IComentarioRepository ComentarioRepository;
        private readonly IMapper mapper;

        public ComentarioService(IComentarioRepository comentarioRepository, IMapper mapper)
        {
            this.ComentarioRepository = comentarioRepository;
            this.mapper = mapper;
        }

        public async Task<List<ComentarioDTO>> GetAll()
        {
            var comentarios = await ComentarioRepository.GetAll();
            List<ComentarioDTO> comentariosDTO = mapper.Map<List<ComentarioDTO>>(comentarios);
            return comentariosDTO;
        }

        public async Task<List<ComentarioDTO>> GetComentariosPorIdPublicacion(int idPublicacion)
        {
            List<Comentario> comentarios = await ComentarioRepository.GetAll();
            List<ComentarioDTO> comentariosDTO = mapper.Map<List<ComentarioDTO>>(comentarios);
            return comentariosDTO;
        }

        public async Task<ComentarioDTO> GetById(int id)
        {
            Comentario comentario = await ComentarioRepository.GetById(id);

            ComentarioDTO comentarioDTO = mapper.Map<ComentarioDTO>(comentario);

            return comentarioDTO;
        }

        public async Task<ComentarioDTO> Create(ComentarioCreacionParaServiceDTO comentarioCreacionDTO)
        {
            ComentarioDTO resultComentarioDTO;
            int response;
            try
            {
                Comentario comentario = mapper.Map<Comentario>(comentarioCreacionDTO);

                comentario.FechaCreacion = comentario.FechaModificacion = DateTime.Now;                 

                response = await ComentarioRepository.Create(comentario);

                resultComentarioDTO = mapper.Map<ComentarioDTO>(comentario);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Concat("Create(ComentarioCreacionDTO comentarioCreacionDTO) Exception: ", exception.Message));
            }

            return resultComentarioDTO;
        }

        public async Task<int> Delete(int id)
        {
            int response;

            response = await ComentarioRepository.Delete(id);

            return response;
        }

        public async Task<ComentarioDTO> Update(ComentarioEdicionDTO comentarioEdicionDTO)
        {
            int response;
            ComentarioDTO comentarioDTO;
            try
            {
                Comentario comentario = mapper.Map<Comentario>(comentarioEdicionDTO);
                comentario.FechaModificacion = DateTime.Now;

                response = await ComentarioRepository.Update(comentario);

                Comentario comentarioEdited = await ComentarioRepository.GetById(response);

                comentarioDTO = mapper.Map<ComentarioDTO>(comentarioEdited);
            }
            catch (Exception exception)
            {

                throw new Exception(string.Concat("ComentarioService.Update(ComentarioEdicionDTO comentarioEdicionDTO) Exception: ", exception.Message));
            }

            return comentarioDTO;
        }

        public async Task<bool> ExisteComentario(int id)
        {
            bool existeComentario = await ComentarioRepository.ExisteComentario(id);

            return existeComentario;
        }        
    }
}