using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.UtilDTOs
{
    public interface IApiResult
    {
        int StatusCode { get; }
        ApiResponseCustom ToResponse();
    }
}
