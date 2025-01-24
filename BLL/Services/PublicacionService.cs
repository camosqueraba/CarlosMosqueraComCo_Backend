using AutoMapper;
using BLL.Interfaces;
using DAL.DTOs.PublicacionDTOs;
using DAL.Model;
using Repository.Interfaces;

namespace BLL.Services
{
    public class PublicacionService : IPublicacionService
    {
        private readonly IPublicacionRepository PublicacionRepository;
        private readonly IMapper mapper;
        
        public PublicacionService(IPublicacionRepository publicacionRepository, IMapper mapper)
        {
            this.PublicacionRepository = publicacionRepository;
            this.mapper = mapper;
        }

        public async Task<List<Publicacion>> GetAll()
        {

            var publicaciones = await PublicacionRepository.GetAll();

            return publicaciones;
        }

        public async Task<PublicacionDTO> GetById(int id)
        {
            Publicacion publicacion = await PublicacionRepository.GetById(id);

            PublicacionDTO publicacionDTO = mapper.Map<PublicacionDTO>(publicacion);

            return publicacionDTO;
        }

        public async Task<PublicacionDTO> Create(PublicacionCreacionDTO publicacionCreacionDTO)
        {
            PublicacionDTO resultPublicacionDTO;
            int response;
            try
            {
                Publicacion publicacion = mapper.Map<Publicacion>(publicacionCreacionDTO);

                publicacion.FechaCreacion = publicacion.FechaModificacion = DateTime.Now;
                
                response = await PublicacionRepository.Create(publicacion);

                resultPublicacionDTO = mapper.Map<PublicacionDTO>(publicacion);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Concat("Create(PublicacionCreacionDTO publicacionCreacionDTO) Exception: ", exception.Message));

            }

            return resultPublicacionDTO;
        }

        public async Task<int> Delete(int id)
        {
            int response;

            response = await PublicacionRepository.Delete(id);

            return response;
        }

        public async Task<PublicacionDTO> Update(PublicacionEdicionDTO publicacionEdicionDTO)
        {
            int response;
            PublicacionDTO publicacionDTO;
            try
            {                
                Publicacion publicacion = mapper.Map<Publicacion>(publicacionEdicionDTO);
                publicacion.FechaModificacion = DateTime.Now;

                response = await PublicacionRepository.Update(publicacion);

                Publicacion publicacionEdited = await PublicacionRepository.GetById(response);

                publicacionDTO = mapper.Map<PublicacionDTO>(publicacionEdited);
            }
            catch (Exception exception)
            {

                throw new Exception(string.Concat("PublicacionService.Update(PublicacionEdicionDTO publicacionEdicionDTO) Exception: ", exception.Message));
            }

            return publicacionDTO;
        }
        
    }
}