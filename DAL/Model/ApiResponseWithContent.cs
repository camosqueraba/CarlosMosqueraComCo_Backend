using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class ApiResponseWithContent <T> : ApiResponse_
    {
        public T Data { get; set; }
        public ApiResponseWithContent(bool success, int statusCode, string title, T datos, List<string> erroresValidacion) : base(success, statusCode, title, erroresValidacion)
        {
            Data = datos;
        }
    }
}