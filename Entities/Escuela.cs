namespace EscuelaMusica.API.Entities{
    public class Escuela
    {
        public int EscuelaId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public List<Profesor> Profesores { get; set; } = new();
        public List<AlumnoEscuela> Alumnos { get; set; } = new();
    }
}