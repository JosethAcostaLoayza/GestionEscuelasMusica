using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EscuelaMusica.API.Data;
using EscuelaMusica.API.Entities;
using EscuelaMusica.API.DTOs;
using EscuelaMusica.API.DTOs.Profesores;

namespace EscuelaMusica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesoresController : ControllerBase
    {
        private readonly EscuelaMusicaContext _context;

        public ProfesoresController(EscuelaMusicaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfesorDTO>>> Get()
        {
            return await _context.Profesores
                .Select(p => new ProfesorDTO { ProfesorId = p.ProfesorId, Nombre = p.Nombre, Apellido = p.Apellido, Identificacion = p.Identificacion, EscuelaId = p.EscuelaId })
                .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProfesorCreateDTO dto)
        {
            if (!await _context.Escuelas.AnyAsync(e => e.EscuelaId == dto.EscuelaId))
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Escuela no encontrada",
                    Status = 400,
                    Detail = $"No existe una escuela con el ID {dto.EscuelaId}.",
                    Instance = HttpContext.Request.Path
                });
            }

            var existe = await _context.Profesores.AnyAsync(p => p.Identificacion == dto.Identificacion);
            if (existe) 
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Identificación duplicada",
                    Status = 400,
                    Detail = $"Ya existe un profesor con la identificación {dto.Identificacion}.",
                    Instance = HttpContext.Request.Path
                });
            }

            var profesor = new Profesor
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Identificacion = dto.Identificacion,
                EscuelaId = dto.EscuelaId
            };

            _context.Profesores.Add(profesor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProfesorUpdateDTO dto)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Profesor no encontrado",
                    Status = 404,
                    Detail = $"No se encontró un profesor con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
            }

            // Validar que la escuela exista si la está actualizando
            if (!await _context.Escuelas.AnyAsync(e => e.EscuelaId == dto.EscuelaId))
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Escuela no encontrada",
                    Status = 400,
                    Detail = $"No existe una escuela con el ID {dto.EscuelaId}.",
                    Instance = HttpContext.Request.Path
                });
            }

            // Validar identificación duplicada (excluyendo el mismo profesor)
            var identificacionDuplicada = await _context.Profesores
                .AnyAsync(p => p.Identificacion == dto.Identificacion && p.ProfesorId != id);
            if (identificacionDuplicada)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Identificación duplicada",
                    Status = 400,
                    Detail = $"Ya existe otro profesor con la identificación {dto.Identificacion}.",
                    Instance = HttpContext.Request.Path
                });
            }

            profesor.Nombre = dto.Nombre;
            profesor.Apellido = dto.Apellido;
            profesor.Identificacion = dto.Identificacion;
            profesor.EscuelaId = dto.EscuelaId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Profesor no encontrado",
                    Status = 404,
                    Detail = $"No se encontró un profesor con el ID {id} para eliminar.",
                    Instance = HttpContext.Request.Path
                });
            }

            _context.Profesores.Remove(profesor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
