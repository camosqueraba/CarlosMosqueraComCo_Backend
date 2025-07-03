using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.UtilDTOs
{
    public class ApiResult<T> : IApiResult
    {
        public bool IsSuccess { get; }
        public int StatusCode { get; }
        public string Message { get; }
        public T? Data { get; }
        public string ErrorMessage { get;  }

        public ApiResult(bool isSuccess, int statusCode, string message, T? data = default, string errorMessage = default)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Message = message;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static ApiResult<T> Ok(T data, string? message = null)
            => new(true, 200, message ?? "Éxito", data);

        public static ApiResult<T> Created(T data, string? message = null) =>
            new(true, 201, message ?? "Recurso creado", data);

        public static ApiResult<T> NotFound(string message = "No encontrado")
            => new(false, 404, message);

        public static ApiResult<T> BadRequest(string message = "Petición inválida")
            => new(false, 400, message);

        public static ApiResult<T> Unauthorized(string message = "No autorizado") =>
        new(false, 401, message, default);

        public static ApiResult<T> Error(string errores, string message = "Error interno", int statusCode = 500) =>
            new(false, statusCode, message, default, errores);

        public static ApiResult<T> Empty(string? message = null) =>
            new(true, 200, message ?? "Sin contenido", default);

        public static ApiResult<T> NoContent(string? message = null) =>
           new(true, 204, message ?? "Sin contenido", default);

        public ApiResponseCustom ToResponse() => new ApiResponseCustom
        {
            Success = IsSuccess,
            StatusCode = StatusCode,
            Message = Message,
            Data = Data,
            ErrorMessage = ErrorMessage
        };
    }
}
