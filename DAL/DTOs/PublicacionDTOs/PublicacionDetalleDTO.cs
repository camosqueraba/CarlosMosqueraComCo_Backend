using DAL.Model;

namespace DAL.DTOs.PublicacionDTOs
{
    public class PublicacionDetalleDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }

    }
}
