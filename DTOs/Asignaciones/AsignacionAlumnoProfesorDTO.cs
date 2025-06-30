
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace EscuelaMusica.API.DTOs.Asignaciones
{
    public class AsignacionAlumnoProfesorDTO
    {
        [Required]
        public int ProfesorId { get; set; }

        [Required]
        public List<int> AlumnoIds { get; set; } = new();
    }
}
