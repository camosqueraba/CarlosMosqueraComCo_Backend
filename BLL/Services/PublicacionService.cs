using AutoMapper;
using BLL.Interfaces;
using DAL.DTOs.PublicacionDTOs;
using DAL.Model.Publicacion;
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

            var clientes = await PublicacionRepository.GetAll();

            return clientes;
        }

        public async Task<Publicacion> GetById(int id)
        {
            var cliente = await PublicacionRepository.GetById(id);

            return cliente;
        }

        public async Task<PublicacionDTO> Create(PublicacionCreacionDTO publicacionCreacionDTO)
        {
            PublicacionDTO resultPublicacionDTO;
            int response;
            try
            {
                Publicacion publicacion = mapper.Map<Publicacion>(publicacionCreacionDTO);

                publicacion.FechaCreacion = DateTime.Now;
                publicacion.FechaModificacion = DateTime.Now;

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

        public async Task<PublicacionDTO> Update(PublicacionEdicionDTO clienteEdicionDTO)
        {
            int response;
            PublicacionDTO clienteDTO;
            try
            {

                //Publicacion clienteActual = await clienteRepository.GetById(clienteEdicionDTO.Id);


                Publicacion cliente = mapper.Map<Publicacion>(clienteEdicionDTO);

                response = await PublicacionRepository.Update(cliente);


                Publicacion clienteEdited = await PublicacionRepository.GetById(response);

                clienteDTO = mapper.Map<PublicacionDTO>(clienteEdited);
            }
            catch (Exception exception)
            {

                throw new Exception(string.Concat("PublicacionService.Update(PublicacionEdicionDTO clienteEdicionDTO) Exception: ", exception.Message));
            }


            return clienteDTO;
        }

        Task<Publicacion> IPublicacionService.Update(PublicacionEdicionDTO publicacion)
        {
            throw new NotImplementedException();
        }
    }
}
