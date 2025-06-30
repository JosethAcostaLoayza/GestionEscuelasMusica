namespace EscuelaMusica.API.DTOs.Escuelas
{
    public class EscuelaDTO
    {
        public int EscuelaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
    }
}
