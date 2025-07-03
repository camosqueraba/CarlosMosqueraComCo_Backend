using AutoMapper;
using BLL.Interfaces;
using DAL.DTOs.PublicacionDTOs;
using DAL.DTOs.UtilDTOs;
using DAL.Model;
using Repository.Interfaces;
using Repository.Repositories;
using System.Collections.Generic;

namespace BLL.Services
{
    public class PublicacionService : IPublicacionService, IPublicacionCustomRepository
    {
        private readonly IPublicacionRepository PublicacionRepository;
        private readonly IMapper mapper;
        
        public PublicacionService(IPublicacionRepository publicacionRepository, IMapper mapper)
        {
            this.PublicacionRepository = publicacionRepository;
            this.mapper = mapper;
        }

        public async Task<ResultadoOperacion<List<PublicacionDTO>>> GetAll()
        {
            ResultadoOperacion<List<PublicacionDTO>> resultadoOperacion = new();

            try
            {
                var resultGetAllPublicacion = await PublicacionRepository.GetAll();

                resultadoOperacion = mapper.Map<ResultadoOperacion<List<PublicacionDTO>>>(resultGetAllPublicacion);
                /*
                if (resultGetAllPublicacion != null && resultGetAllPublicacion.OperacionCompletada)
                {
                    List<PublicacionDTO> publicacionesDTO = mapper.Map<List<PublicacionDTO>>(resultGetAllPublicacion.DatosResultado);
                    resultadoOperacion.DatosResultado = publicacionesDTO;
                    resultadoOperacion.OperacionCompletada = true;
                }
                */
                //resultadoOperacion.OperacionCompletada = resultGetAllPublicacion.OperacionCompletada;
            }
            catch (Exception ex)
            {
                resultadoOperacion.Error = string.Concat(ex.Message, " | ", ex.InnerException);
                resultadoOperacion.Origen = "PublicacionService.GetAll()";
            }
            
            return resultadoOperacion;
        }

        public async Task<ResultadoOperacion<PublicacionDetalleDTO>> GetById(int id)
        {
            ResultadoOperacion<PublicacionDetalleDTO> resultadoOperacionService = new();
            try
            {                
                ResultadoOperacion<Publicacion> resultadoOperacionRepository;
                resultadoOperacionRepository = await PublicacionRepository.GetById(id);

                if (resultadoOperacionRepository != null && resultadoOperacionRepository.OperacionCompletada == true)
                {
                    resultadoOperacionService.DatosResultado = mapper.Map<PublicacionDetalleDTO>(resultadoOperacionRepository.DatosResultado);
                    resultadoOperacionService.OperacionCompletada = true;
                    //resultadoOperacionService.DatosResultado = resultPublicacionDTO;
                }
                else
                {
                    resultadoOperacionService.OperacionCompletada = false;
                    resultadoOperacionService.DatosResultado = null;
                    resultadoOperacionService.Origen = resultadoOperacionRepository.Origen;
                    resultadoOperacionService.Error = resultadoOperacionRepository.Error;
                }
                //Publicacion publicacion = await PublicacionRepository.GetById(id);
                //PublicacionDetalleDTO publicacionDetalleDTO = mapper.Map<PublicacionDetalleDTO>(publicacion);
            }
            catch (Exception)
            {

                throw;
            }
            

            return resultadoOperacionService;
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

        public async Task<ResultadoOperacion<int>> Delete(int id)
        {
            ResultadoOperacion<int> response;

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

                Publicacion publicacionEdited;
                ResultadoOperacion<Publicacion> resultadoOperacion = await PublicacionRepository.GetById(response);
                publicacionEdited = resultadoOperacion.DatosResultado;
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