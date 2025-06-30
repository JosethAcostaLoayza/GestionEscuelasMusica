using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EscuelaMusica.API.Data;
using EscuelaMusica.API.Entities;
using EscuelaMusica.API.DTOs.Alumnos;

namespace EscuelaMusica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnosController : ControllerBase
    {
        private readonly EscuelaMusicaContext _context;

        public AlumnosController(EscuelaMusicaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlumnoDTO>>> Get()
        {
            return await _context.Alumnos
                .Select(a => new AlumnoDTO { AlumnoId = a.AlumnoId, Nombre = a.Nombre, Apellido = a.Apellido, FechaNacimiento = a.FechaNacimiento, Identificacion = a.Identificacion })
                .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlumnoCreateDTO dto)
        {
            if (await _context.Alumnos.AnyAsync(a => a.Identificacion == dto.Identificacion))
                return BadRequest(new ProblemDetails
                {
                    Title = "Identificación duplicada",
                    Status = 400,
                    Detail = $"Ya existe un alumno registrado con la identificación '{dto.Identificacion}'.",
                    Instance = HttpContext.Request.Path
                });

            var alumno = new Alumno
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FechaNacimiento = dto.FechaNacimiento,
                Identificacion = dto.Identificacion
            };

            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AlumnoUpdateDTO dto)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Alumno no encontrado",
                    Status = 404,
                    Detail = $"No existe un alumno con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
            }

            // Validar que la nueva identificación no sea duplicada en otro alumno
            if (await _context.Alumnos.AnyAsync(a => a.Identificacion == dto.Identificacion && a.AlumnoId != id))
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Identificación duplicada",
                    Status = 400,
                    Detail = $"La identificación '{dto.Identificacion}' ya está registrada para otro alumno.",
                    Instance = HttpContext.Request.Path
                });
            }

            alumno.Nombre = dto.Nombre;
            alumno.Apellido = dto.Apellido;
            alumno.FechaNacimiento = dto.FechaNacimiento;
            alumno.Identificacion = dto.Identificacion;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Alumno no encontrado",
                    Status = 404,
                    Detail = $"No existe un alumno con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
            }

            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
