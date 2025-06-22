namespace DAL.DTOs.UtilDTOs
{
    public class ResultadoOperacion<T>
    {
        public bool OperacionCompletada { get; set; }
        public T DatosResultado { get; set; }
        public string Origen {  get; set; }
        public string Error { get; set; }
    }
}