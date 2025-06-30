namespace EscuelaMusica.API.Entities{
    public class AlumnoProfesor
    {
        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; } = null!;

        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; } = null!;
    }
}