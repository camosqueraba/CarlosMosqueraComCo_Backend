using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class ResultadoOperacion<T>
    {
        public bool OperacionCompletada { get; set; }
        public T DatosResultado { get; set; }
        public string Origen {  get; set; }
        public string Error { get; set; }
    }
}
