namespace EscuelaMusica.API.Entities
{
    public class AlumnoEscuela
    {
        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; } = null!;

        public int EscuelaId { get; set; }
        public Escuela Escuela { get; set; } = null!;

        public DateTime FechaInscripcion { get; set; } = DateTime.Now;
    }
}
