using System.ComponentModel.DataAnnotations;
namespace EscuelaMusica.API.DTOs.Asignaciones
{
    public class InscripcionAlumnoEscuelaDTO
    {
        public int AlumnoId { get; set; }
        public int EscuelaId { get; set; }
    }
}
