using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.UsuarioDTOs
{
    public class UsuarioEdicionDTO
    {
        public string Id { get; set; }

        [Display(Name = "Nombre de usuario")]
        [Required(ErrorMessage = "el campo {0} es requerido")]
        [StringLength(250, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(250, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; }

        [Display(Name = "Numero de telefono")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(15, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        //[Phone(ErrorMessage = "Telefono invalido")]
        public string PhoneNumber { get; set; }
        
    }
}
