using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.UtilDTOs
{
    public class ApiResponseCustom
    {
        public bool Success { get; init; }
        public int StatusCode { get; init; }
        public string Message { get; init; } = string.Empty;
        public object? Data { get; init; }
        public string ErrorMessage { get; init; } = string.Empty;

        /*
        public static ApiResponseCustom SuccessResponse(object? data, int statusCode = 200, string? message = null) =>
            new()
            {
                Success = true,
                StatusCode = statusCode,
                Message = message ?? "Operación exitosa.",
                Data = data
            };

        public static ApiResponseCustom ErrorResponse(string message, int statusCode) =>
            new()
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                Data = null
            };

        */
    }
}
