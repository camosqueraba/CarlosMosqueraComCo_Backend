using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.ComentarioDTOs
{
    public class ComentarioCreacionDTO
    {       

        [Display(Name = "descripcion")]
        [Required(ErrorMessage = "el campo {0} es requerido")]
        public string Contenido { get; set; }

        [Display(Name = "publicacion id")]
        [Required(ErrorMessage = "el campo {0} es requerido")]
        public int PublicacionId { get; set; }
    }
}