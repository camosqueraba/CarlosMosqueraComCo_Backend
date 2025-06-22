using DAL.DTOs.AutenticacionDTOs;
using DAL.DTOs.UsuarioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAutorizacionUtilsRepository
    {
        public Task<RespuestaAutenticacionDTO> ConstruirToken(CredencialesUsuarioDTO credencialesUsuarioDTO);

        public Task<bool> CheckPasswordIdentity(CredencialesUsuarioDTO credencialesUsuarioDTO);
    }
}
