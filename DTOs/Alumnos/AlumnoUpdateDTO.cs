using System.ComponentModel.DataAnnotations;
namespace EscuelaMusica.API.DTOs.Alumnos
{
    public class AlumnoUpdateDTO
    {
        [Required]
        public int AlumnoId { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string Apellido { get; set; } = null!;

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string Identificacion { get; set; } = null!;
    }
}
