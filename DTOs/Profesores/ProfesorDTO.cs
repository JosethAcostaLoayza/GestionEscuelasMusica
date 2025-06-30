namespace EscuelaMusica.API.DTOs.Profesores
{
    public class ProfesorDTO
    {
        public int ProfesorId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public int EscuelaId { get; set; }
    }
}
