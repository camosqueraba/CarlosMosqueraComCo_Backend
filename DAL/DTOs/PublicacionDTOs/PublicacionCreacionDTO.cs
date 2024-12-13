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
        public string Titulo { get; set; }

        [Display(Name = "descripcion")]
        [Required(ErrorMessage = "el campo {0} es requerido")]
        public string Descripcion { get; set; }

        /*
        [Display(Name = "canon")]
        [Required(ErrorMessage = "el campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "el campo {0} solo permite valores entre {1} y {2}")]
        public int Canon { get; set; }
        */
    }
}