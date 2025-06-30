using System.ComponentModel.DataAnnotations;
namespace EscuelaMusica.API.DTOs.Escuelas
{
    public class EscuelaUpdateDTO
    {
        [Required]
        public int EscuelaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;
    }
}