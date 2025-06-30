using System.ComponentModel.DataAnnotations;
namespace EscuelaMusica.API.DTOs.Profesores
{
    public class ProfesorCreateDTO
    {
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
