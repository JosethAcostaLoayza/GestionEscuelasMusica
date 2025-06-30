
using EscuelaMusica.API.DTOs.Alumnos;
using EscuelaMusica.API.DTOs.Escuelas;
using EscuelaMusica.API.DTOs.Profesores;
namespace EscuelaMusica.API.DTOs.Consultas
{
    public class ProfesorConEscuelaYAlumnosDTO
    {
        public ProfesorDTO Profesor { get; set; } = new();
        public EscuelaDTO Escuela { get; set; } = new();
        public List<AlumnoDTO> Alumnos { get; set; } = new();
    }
}
