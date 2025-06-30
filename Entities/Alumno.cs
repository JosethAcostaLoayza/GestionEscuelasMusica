namespace EscuelaMusica.API.Entities{
    public class Alumno
    {
        public int AlumnoId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Identificacion { get; set; } = null!;

        public List<AlumnoProfesor> Profesores { get; set; } = new();
        public List<AlumnoEscuela> Escuelas { get; set; } = new();
    }
}