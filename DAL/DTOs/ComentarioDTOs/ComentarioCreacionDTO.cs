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

        [Display(Name = "contenido")]
        [Required(ErrorMessage = "el campo {0} es requerido")]
        public string Contenido { get; set; }        
    }
}