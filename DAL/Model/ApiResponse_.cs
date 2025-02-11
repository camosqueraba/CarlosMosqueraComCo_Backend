using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class ApiResponse_
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Title { get; set; }        
        public List<string> Errores { get; set; }
        //public ResultadoOperacion<T> ResultadoOperacion { get; set; }

        public ApiResponse_(bool success, int statusCode, string title, List<string> erroresValidacion)
        {
            Success = success;
            StatusCode = statusCode;
            Title = title;
            
            Errores = erroresValidacion;
        }
    }
}