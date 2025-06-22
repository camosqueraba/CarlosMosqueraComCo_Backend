using BLL.Interfaces;
using DAL.DTOs.AutenticacionDTOs;
using DAL.DTOs.UsuarioDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BLL.Services
{
    public class  AutorizacionUtilsService : IAutorizacionUtilsService
    {
        private readonly IAutorizacionUtilsRepository AutorizacionUtilsRepository;
        //private readonly IAutorizacionUtilsService AutorizacionUtilsService_; 
        private readonly IUsuarioService UsuarioService;
        public AutorizacionUtilsService(IAutorizacionUtilsRepository autorizacionUtilsRepository, IUsuarioService usuarioService)
        {
            AutorizacionUtilsRepository = autorizacionUtilsRepository;
            UsuarioService = usuarioService;
        }
        
        public async Task<RespuestaAutenticacionDTO> ConstruirToken(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            RespuestaAutenticacionDTO respuesta = await AutorizacionUtilsRepository.ConstruirToken(credencialesUsuarioDTO);
            return respuesta;
        }

        /*
        public async Task<RespuestaAutenticacionDTO> ValidarPasswordLogin(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            bool result = await AutorizacionUtilsService_.ValidarPassworLogin(credencialesUsuarioDTO);

            return null;
        }
        */
        
        public async Task<RespuestaAutenticacionDTO> LoginAutorizacionUtilsService(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            RespuestaAutenticacionDTO respuestaAutenticacion = new RespuestaAutenticacionDTO();

            bool usuario = await UsuarioService.ExisteUsuario(credencialesUsuarioDTO.Email);
            
            if (!usuario)
            {
                return RetornarLoginIncorrecto();
            }

            bool resultadoValidarPassword = await AutorizacionUtilsRepository.CheckPasswordIdentity(credencialesUsuarioDTO);

            if (!resultadoValidarPassword)
            {
                return RetornarLoginIncorrecto();
                
            }

            respuestaAutenticacion = await AutorizacionUtilsRepository.ConstruirToken(credencialesUsuarioDTO);

            if(respuestaAutenticacion != null && !string.IsNullOrEmpty(respuestaAutenticacion.Token))
            {
                respuestaAutenticacion.AutenticacionCorrecta = true;
                respuestaAutenticacion.MensajeResultado = "Login correcto";
            }

            return respuestaAutenticacion;
        }

        private RespuestaAutenticacionDTO RetornarLoginIncorrecto()
        {
            return new RespuestaAutenticacionDTO() { AutenticacionCorrecta = false, MensajeResultado = "Login incorrecto" };
        }
    }
}
