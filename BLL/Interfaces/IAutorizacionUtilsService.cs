using DAL.DTOs.AutenticacionDTOs;
using DAL.DTOs.UsuarioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAutorizacionUtilsService
    {
        public Task<RespuestaAutenticacionDTO> ConstruirToken(CredencialesUsuarioDTO credencialesUsuarioDTO);
        public Task<RespuestaAutenticacionDTO> LoginAutorizacionUtilsService(CredencialesUsuarioDTO credencialesUsuarioDTO);
    }
}
