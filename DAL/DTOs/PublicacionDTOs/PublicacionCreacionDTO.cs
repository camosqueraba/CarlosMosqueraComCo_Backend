using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Validaciones;

namespace DAL.DTOs.PublicacionDTOs
{
    public class PublicacionCreacionDTO
    {
        [Display(Name = "titulo")]
        [Required(ErrorMessage = "el campo {0} es requerido")]
        [StringLength(250, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Titulo { get; set; }

        [Display(Name = "contenido")]
        [Required(ErrorMessage = "el campo {0} es requerido")]
        public string Contenido { get; set; }
                
    }
}