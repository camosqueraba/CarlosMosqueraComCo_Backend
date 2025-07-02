using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.UtilDTOs
{
    public interface IApiResponse
    {
        public bool Success { get; }
        public int StatusCode { get; }
        public string Title { get; }
        public object  Data { get; }
        public List<string> Errores { get; }
    }
}
