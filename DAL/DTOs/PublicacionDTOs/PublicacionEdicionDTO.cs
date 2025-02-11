using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.PublicacionDTOs
{
    public class PublicacionEdicionDTO
    {
        public int Id { get; set; }
        [Display(Name = "titulo")]
        [Required(ErrorMessage = "el campo {0} es requerido")]
        [StringLength(250, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Titulo { get; set; }

        [Display(Name = "contenido")]
        [Required(ErrorMessage = "el campo {0} es requerido")]
        public string Contenido { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
