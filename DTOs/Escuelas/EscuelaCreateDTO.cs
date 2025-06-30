using System.ComponentModel.DataAnnotations;
namespace EscuelaMusica.API.DTOs.Escuelas
{
    public class EscuelaCreateDTO
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        [Required]
        [StringLength(255)]
        public string Codigo { get; set; } = null!;
    }
}
