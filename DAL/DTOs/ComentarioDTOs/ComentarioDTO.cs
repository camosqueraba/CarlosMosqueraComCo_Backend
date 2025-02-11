using DAL.Model;
using System.ComponentModel.DataAnnotations;

namespace DAL.DTOs.ComentarioDTOs
{
    public class ComentarioDTO
    {
        public int Id { get; set; }
        [Required]
        public string Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        [Required]
        public int PublicacionId { get; set; }
        public Publicacion? Publicacion { get; set; }
    }
}