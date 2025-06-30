namespace EscuelaMusica.API.Entities{
    public class Profesor
    {
        public int ProfesorId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Identificacion { get; set; } = null!;

        public int EscuelaId { get; set; }
        public Escuela Escuela { get; set; } = null!;

        public List<AlumnoProfesor> Alumnos { get; set; } = new();
    }
}