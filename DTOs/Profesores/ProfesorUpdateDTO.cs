using System.ComponentModel.DataAnnotations;
namespace EscuelaMusica.API.DTOs.Profesores
{
    public class ProfesorUpdateDTO
    {
        [Required]
        public int ProfesorId { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string Apellido { get; set; } = null!;

        [Required]
        public string Identificacion { get; set; } = null!;

        [Required]
        public int EscuelaId { get; set; }
    }
}