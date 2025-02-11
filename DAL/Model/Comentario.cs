using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace DAL.Model
{
    public class Comentario
    {
        public int Id { get; set; }
        [Required]
        public string Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        [Required]
        public int PublicacionId { get; set; }
        //public Publicacion? Publicacion { get; set; }
    }
}
