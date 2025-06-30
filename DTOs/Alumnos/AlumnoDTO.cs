namespace EscuelaMusica.API.DTOs.Alumnos
{
    public class AlumnoDTO
    {
        public int AlumnoId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Identificacion { get; set; } = null!;
    }
}
