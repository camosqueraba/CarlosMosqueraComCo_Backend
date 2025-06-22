using AutoMapper;
using BLL.Interfaces;
using DAL.DTOs.AutenticacionDTOs;
using DAL.DTOs.UsuarioDTOs;
using DAL.DTOs.UtilDTOs;
using DAL.Model;
using Microsoft.AspNetCore.Identity;
using Repository.Interfaces;
using Repository.Repositories;

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

        public async Task<ResultadoOperacion<List<UsuarioDTO>>> GetAll()
        {
            ResultadoOperacion<List<UsuarioDTO>> resultadoOperacion = new();
            try
            {
                var resultadoOperacionRepository = await UsuarioRepository.GetAll();
                if(resultadoOperacionRepository != null && resultadoOperacionRepository.OperacionCompletada)
                {
                    //resultadoOperacion.DatosResultado = (List<UsuarioDTO>)resultadoOperacionRepository.DatosResultado.Select(identityUser => identityUser);
                    resultadoOperacion.DatosResultado = resultadoOperacionRepository.DatosResultado
                        .Select(identityUser => new UsuarioDTO {
                            Id = identityUser.Id,                                        
                            UserName = identityUser.UserName,
                            Email = identityUser.Email
                        }).ToList();

                    resultadoOperacion.OperacionCompletada = true;
                }
                //List<UsuarioDTO> publicacionesDTO = mapper.Map<List<UsuarioDTO>>(usuarios);
                //return publicacionesDTO;
            }
            catch (Exception ex)
            {
                resultadoOperacion.Origen = "UsuarioService.GetAll";
                resultadoOperacion.Error = ex.Message;
            }
            return resultadoOperacion;
        }

        
        public async Task<ResultadoOperacion<UsuarioDetalleDTO>> GetById(string id)
        {
            ResultadoOperacion<UsuarioDetalleDTO> resultadoOperacionService = new();
            try
            {
                ResultadoOperacion<IdentityUser> resultadoOperacionRepository;
                resultadoOperacionRepository = await UsuarioRepository.GetById(id);

                if (resultadoOperacionRepository != null &&
                    resultadoOperacionRepository.OperacionCompletada &&
                    resultadoOperacionRepository.DatosResultado != null )
                {
                    resultadoOperacionService.DatosResultado = new UsuarioDetalleDTO
                    {                    
                        UserName            = resultadoOperacionRepository.DatosResultado.UserName,
                        NormalizedUserName  = resultadoOperacionRepository.DatosResultado.NormalizedUserName,
                        Email               = resultadoOperacionRepository.DatosResultado.Email,
                        NormalizedEmail     = resultadoOperacionRepository.DatosResultado.NormalizedEmail,
                        PhoneNumber         = resultadoOperacionRepository.DatosResultado.PhoneNumber,
                        TwoFactorEnabled    = resultadoOperacionRepository.DatosResultado.TwoFactorEnabled,
                        LockoutEnd          = resultadoOperacionRepository.DatosResultado.LockoutEnd,
                        AccessFailedCount   = resultadoOperacionRepository.DatosResultado.AccessFailedCount,

                    };
                    // mapper.Map<UsuarioDetalleDTO>(resultadoOperacionRepository.DatosResultado);

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
            catch (Exception ex)
            {
                resultadoOperacionService.Origen = "UsuarioService.GetById";
                resultadoOperacionService.Error = ex.Message;
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
            ResultadoOperacion<string> resultadoOperacionRepository;
            try
            {
                Usuario usuario = mapper.Map<Usuario>(usuarioCreacionDTO);

                //usuario.FechaCreacion = usuario.FechaModificacion = DateTime.Now;

                resultadoOperacionRepository = await UsuarioRepository.Create(usuario);

                if (resultadoOperacionRepository != null && resultadoOperacionRepository.OperacionCompletada == true)
                {
                    resultUsuarioDTO = mapper.Map<UsuarioDTO>(usuario);
                    resultUsuarioDTO.Id = resultadoOperacionRepository.DatosResultado;
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

        public async Task<ResultadoOperacion<RespuestaAutenticacionDTO>> RegistrarUsuario(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            return await UsuarioRepository.RegistrarUsuario(credencialesUsuarioDTO);
        }
    }
}