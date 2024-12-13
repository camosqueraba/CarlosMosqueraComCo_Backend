namespace DAL.Model
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Title { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse(bool success, int statusCode, string title, T data, List<string> errors)
        {
            Success = success;
            StatusCode = statusCode;
            Title = title;
            Data = data;
            Errors = errors;
        }
    }
}