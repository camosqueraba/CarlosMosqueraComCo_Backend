﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.UtilDTOs
{
    public class ApiResponse_<T> : IApiResponse
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Title { get; set; }
        public T? Data { get; }
        public List<string> Errores { get; set; }

        object IApiResponse.Data => Data;

        //public ResultadoOperacion<T> ResultadoOperacion { get; set; }

        public ApiResponse_(bool success, int statusCode, string title, T data,  List<string> erroresValidacion)
        {
            Success = success;
            StatusCode = statusCode;
            Title = title;
            Data = data;
            Errores = erroresValidacion;
        }
    }
}