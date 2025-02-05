using AutoMapper;
using BLL.Interfaces;
using DAL.DTOs.PublicacionDTOs;
using DAL.Model;
using Repository.Interfaces;
using Repository.Repositories;

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

        public async Task<List<PublicacionDTO>> GetAll()
        {

            var publicaciones = await PublicacionRepository.GetAll();
            List<PublicacionDTO> publicacionesDTO = mapper.Map<List<PublicacionDTO>>(publicaciones);
            return publicacionesDTO;
        }

        public async Task<PublicacionDetalleDTO> GetById(int id)
        {
            Publicacion publicacion = await PublicacionRepository.GetById(id);
            PublicacionDetalleDTO publicacionDetalleDTO = mapper.Map<PublicacionDetalleDTO>(publicacion);

            return publicacionDetalleDTO;
        }
        /*
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
        */
        public async Task<ResultadoOperacion<PublicacionDTO>> Create(PublicacionCreacionDTO publicacionCreacionDTO)
        {
            PublicacionDTO resultPublicacionDTO;
            ResultadoOperacion<PublicacionDTO> resultadoOperacionService = new();
            ResultadoOperacion<int> resultadoOperacionRepository;
            try
            {
                Publicacion publicacion = mapper.Map<Publicacion>(publicacionCreacionDTO);

                publicacion.FechaCreacion = publicacion.FechaModificacion = DateTime.Now;

                resultadoOperacionRepository = await PublicacionRepository.Create(publicacion);

                if (resultadoOperacionRepository != null && resultadoOperacionRepository.OperacionCompletada == true)
                {
                    resultPublicacionDTO = mapper.Map<PublicacionDTO>(publicacion);
                    resultadoOperacionService.OperacionCompletada = true;
                    resultadoOperacionService.DatosResultado = resultPublicacionDTO;                    
                }
                else
                {
                    resultadoOperacionService.OperacionCompletada = false;
                    resultadoOperacionService.DatosResultado = null;
                    resultadoOperacionService.Origen = resultadoOperacionRepository.Origen;
                    resultadoOperacionService.Error = resultadoOperacionRepository.Error;
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(string.Concat("Create(PublicacionCreacionDTO publicacionCreacionDTO) Exception: ", exception.Message));
                resultadoOperacionService.OperacionCompletada = false;
                resultadoOperacionService.DatosResultado = null;
                resultadoOperacionService.Origen = "PublicacionService.Create";
                resultadoOperacionService.Error = ex.Message;
            }

            return resultadoOperacionService;
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

        public async Task<bool> ExistePublicacion(int id)
        {
            bool existePublicacion = await PublicacionRepository.ExistePublicacion(id);

            return existePublicacion;
        }

    }
}