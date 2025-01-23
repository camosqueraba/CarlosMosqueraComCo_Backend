using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.PublicacionDTOs
{
    public class PublicacionCreacionDTO
    {
        [Display(Name = "titulo")]
        [Required(ErrorMessage = "el campo {0} es requerido")]
        [StringLength(250, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Titulo { get; set; }

        [Display(Name = "descripcion")]
        [Required(ErrorMessage = "el campo {0} es requerido")]
        public string Descripcion { get; set; }
                
    }
}