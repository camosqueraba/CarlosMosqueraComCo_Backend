using System.ComponentModel.DataAnnotations;

namespace DAL.DTOs.UsuarioDTOs
{
    public class CredencialesUsuarioDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}