using AutoMapper;
using BLL.Interfaces;
using DAL.DTOs.UsuarioDTOs;
using DAL.Model;
using Microsoft.AspNetCore.Identity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository UsuarioRepository;
        private readonly IMapper mapper;

        public UsuarioService(IUsuarioRepository publicacionRepository, IMapper mapper)
        {
            this.UsuarioRepository = publicacionRepository;
            this.mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> GetAll()
        {

            var usuarios = await UsuarioRepository.GetAll();
            List<UsuarioDTO> publicacionesDTO = mapper.Map<List<UsuarioDTO>>(usuarios);
            return publicacionesDTO;
        }

        
        public async Task<ResultadoOperacion<UsuarioDetalleDTO>> GetById(int id)
        {
            ResultadoOperacion<UsuarioDetalleDTO> resultadoOperacionService = new();
            try
            {
                ResultadoOperacion<IdentityUser> resultadoOperacionRepository;
                resultadoOperacionRepository = await UsuarioRepository.GetById(id);

                if (resultadoOperacionRepository != null && resultadoOperacionRepository.OperacionCompletada == true)
                {
                    resultadoOperacionService.DatosResultado = mapper.Map<UsuarioDetalleDTO>(resultadoOperacionRepository.DatosResultado);
                    resultadoOperacionService.OperacionCompletada = true;
                    //resultadoOperacionService.DatosResultado = resultUsuarioDTO;
                }
                else
                {
                    resultadoOperacionService.OperacionCompletada = false;
                    resultadoOperacionService.DatosResultado = null;
                    resultadoOperacionService.Origen = resultadoOperacionRepository.Origen;
                    resultadoOperacionService.Error = resultadoOperacionRepository.Error;
                }
                //IdentityUser usuario = await UsuarioRepository.GetById(id);
                //UsuarioDetalleDTO publicacionDetalleDTO = mapper.Map<UsuarioDetalleDTO>(usuario);
            }
            catch (Exception)
            {

                throw;
            }


            return resultadoOperacionService;
        }
        

        /*
        public async Task<UsuarioDTO> Create(UsuarioCreacionDTO publicacionCreacionDTO)
        {
            UsuarioDTO resultUsuarioDTO;
            int response;
            try
            {
                IdentityUser usuario = mapper.Map<IdentityUser>(publicacionCreacionDTO);

                usuario.FechaCreacion = usuario.FechaModificacion = DateTime.Now;
                
                response = await UsuarioRepository.Create(usuario);

                resultUsuarioDTO = mapper.Map<UsuarioDTO>(usuario);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Concat("Create(UsuarioCreacionDTO publicacionCreacionDTO) Exception: ", exception.Message));

            }

            return resultUsuarioDTO;
        }
        */


        public async Task<ResultadoOperacion<UsuarioDTO>> Create(UsuarioCreacionDTO usuarioCreacionDTO)
        {
            UsuarioDTO resultUsuarioDTO;
            ResultadoOperacion<UsuarioDTO> resultadoOperacionService = new();
            ResultadoOperacion<int> resultadoOperacionRepository;
            try
            {
                Usuario usuario = mapper.Map<Usuario>(usuarioCreacionDTO);

                //usuario.FechaCreacion = usuario.FechaModificacion = DateTime.Now;

                resultadoOperacionRepository = await UsuarioRepository.Create(usuario);

                if (resultadoOperacionRepository != null && resultadoOperacionRepository.OperacionCompletada == true)
                {
                    resultUsuarioDTO = mapper.Map<UsuarioDTO>(usuario);
                    resultadoOperacionService.OperacionCompletada = true;
                    resultadoOperacionService.DatosResultado = resultUsuarioDTO;
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
                //throw new Exception(string.Concat("Create(UsuarioCreacionDTO publicacionCreacionDTO) Exception: ", exception.Message));
                resultadoOperacionService.OperacionCompletada = false;
                resultadoOperacionService.DatosResultado = null;
                resultadoOperacionService.Origen = "UsuarioService.Create";
                resultadoOperacionService.Error = ex.Message;
            }

            return resultadoOperacionService;
        }

        /*
        public async Task<ResultadoOperacion<int>> Delete(int id)
        {
            ResultadoOperacion<int> response;

            response = await UsuarioRepository.Delete(id);

            return response;
        }
        */

        /*
        public async Task<UsuarioDTO> Update(UsuarioEdicionDTO publicacionEdicionDTO)
        {
            int response;
            UsuarioDTO publicacionDTO;
            try
            {
                IdentityUser usuario = mapper.Map<IdentityUser>(publicacionEdicionDTO);
                usuario.FechaModificacion = DateTime.Now;

                response = await UsuarioRepository.Update(usuario);

                IdentityUser publicacionEdited;
                ResultadoOperacion<IdentityUser> resultadoOperacion = await UsuarioRepository.GetById(response);
                publicacionEdited = resultadoOperacion.DatosResultado;
                publicacionDTO = mapper.Map<UsuarioDTO>(publicacionEdited);
            }
            catch (Exception exception)
            {

                throw new Exception(string.Concat("UsuarioService.Update(UsuarioEdicionDTO publicacionEdicionDTO) Exception: ", exception.Message));
            }

            return publicacionDTO;
        }
        */

        public async Task<bool> ExisteUsuario(string email)
        {
            bool existeUsuario = await UsuarioRepository.ExisteUsuario(email);

            return existeUsuario;
        }

    }
}