using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.AutenticacionDTOs
{
    public class RespuestaAutenticacionDTO
    {
        public bool AutenticacionCorrecta { get; set; }
        public string MensajeResultado { get; set; }
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}