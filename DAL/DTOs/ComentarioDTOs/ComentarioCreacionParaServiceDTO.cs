using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.ComentarioDTOs
{
    public class ComentarioCreacionParaServiceDTO : ComentarioCreacionDTO
    {        
        
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }        
        public int PublicacionId { get; set; }
    }
}
