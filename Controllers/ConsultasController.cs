using EscuelaMusica.API.Data;
using EscuelaMusica.API.DTOs.Consultas;
using EscuelaMusica.API.DTOs.Profesores;
using EscuelaMusica.API.DTOs.Escuelas;
using EscuelaMusica.API.DTOs.Alumnos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscuelaMusica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultasController : ControllerBase
    {
        private readonly EscuelaMusicaContext _context;

        public ConsultasController(EscuelaMusicaContext context)
        {
            _context = context;
        }

        // Consulta 1: Alumnos inscritos por profesor y la escuela a la que pertenece
        [HttpGet("alumnos-por-profesor/{profesorId}")]
        public async Task<ActionResult<ProfesorConEscuelaYAlumnosDTO>> GetAlumnosPorProfesor(int profesorId)
        {
            var result = await _context.Profesores
                .Include(p => p.Escuela)
                .Include(p => p.Alumnos) // navegación muchos a muchos
                    .ThenInclude(ap => ap.Alumno)
                .Where(p => p.ProfesorId == profesorId)
                .Select(p => new ProfesorConEscuelaYAlumnosDTO
                {
                    Profesor = new ProfesorDTO
                    {
                        ProfesorId = p.ProfesorId,
                        Nombre = p.Nombre,
                        Apellido = p.Apellido,
                        Identificacion = p.Identificacion
                    },
                    Escuela = new EscuelaDTO
                    {
                        EscuelaId = p.Escuela!.EscuelaId,
                        Codigo = p.Escuela.Codigo,
                        Nombre = p.Escuela.Nombre,
                        Descripcion = p.Escuela.Descripcion
                    },
                    Alumnos = p.Alumnos
                        .Select(ap => new AlumnoDTO
                        {
                            AlumnoId = ap.Alumno.AlumnoId,
                            Nombre = ap.Alumno.Nombre,
                            Apellido = ap.Alumno.Apellido,
                            Identificacion = ap.Alumno.Identificacion
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Profesor no encontrado",
                    Status = 404,
                    Detail = $"No se encontró un profesor con el ID {profesorId}.",
                    Instance = HttpContext.Request.Path
                });
            }

            return Ok(result);
        }

        // Consulta 2: Escuelas y alumnos por profesor (mismo resultado, mismo DTO)
        [HttpGet("escuela-y-alumnos-por-profesor/{profesorId}")]
        public async Task<ActionResult<ProfesorConEscuelaYAlumnosDTO>> GetEscuelaYAlumnosPorProfesor(int profesorId)
        {
            // Reutiliza la lógica anterior, ya que el resultado es el mismo
            return await GetAlumnosPorProfesor(profesorId);
        }
    }
}
