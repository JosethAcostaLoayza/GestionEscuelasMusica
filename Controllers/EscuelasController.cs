using Microsoft.AspNetCore.Mvc;
using EscuelaMusica.API.DTOs.Escuelas;
using EscuelaMusica.API.Entities;
using EscuelaMusica.API.Services.Interfaces;

namespace EscuelaMusica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EscuelasController : ControllerBase
    {
        private readonly IEscuelaService _escuelaService;

        public EscuelasController(IEscuelaService escuelaService)
        {
            _escuelaService = escuelaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscuelaDTO>>> GetEscuelas()
        {
            var escuelas = await _escuelaService.GetAllAsync();

            var dtoList = escuelas.Select(e => new EscuelaDTO
            {
                EscuelaId = e.EscuelaId,
                Nombre = e.Nombre,
                Codigo = e.Codigo,
                Descripcion = e.Descripcion
            });

            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EscuelaDTO>> GetEscuela(int id)
        {
            var escuela = await _escuelaService.GetByIdAsync(id);
            if (escuela == null)
            {
                return NotFound(new
                {
                    type = "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    title = "Escuela no encontrada",
                    status = 404,
                    detail = $"No se encontró una escuela con el ID {id}.",
                    traceId = HttpContext.TraceIdentifier
                });
            }

            return Ok(new EscuelaDTO
            {
                EscuelaId = escuela.EscuelaId,
                Nombre = escuela.Nombre,
                Codigo = escuela.Codigo,
                Descripcion = escuela.Descripcion
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EscuelaCreateDTO dto)
        {
            try
            {
                var nueva = new Escuela
                {
                    Nombre = dto.Nombre,
                    Codigo = dto.Codigo,
                    Descripcion = dto.Descripcion
                };

                var creada = await _escuelaService.CreateAsync(nueva);
                return CreatedAtAction(nameof(GetEscuela), new { id = creada.EscuelaId }, null);
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Error al crear la escuela",
                    Status = 400,
                    Detail = ex.Message,
                    Instance = HttpContext.Request.Path
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EscuelaUpdateDTO dto)
        {
            var escuela = await _escuelaService.GetByIdAsync(id);
            if (escuela == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Escuela no encontrada",
                    Status = 404,
                    Detail = $"No se encontró una escuela con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
            }

            escuela.Nombre = dto.Nombre;
            escuela.Descripcion = dto.Descripcion;

            try
            {
                var updated = await _escuelaService.UpdateAsync(escuela);
                if (!updated)
                {
                    return BadRequest(new ProblemDetails
                    {
                        Title = "Actualización fallida",
                        Status = 400,
                        Detail = "No se pudo actualizar la escuela.",
                        Instance = HttpContext.Request.Path
                    });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Error al actualizar la escuela",
                    Status = 400,
                    Detail = ex.Message,
                    Instance = HttpContext.Request.Path
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _escuelaService.DeleteAsync(id);
            if (!eliminado)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Escuela no encontrada",
                    Status = 404,
                    Detail = $"No se encontró una escuela con el ID {id} para eliminar.",
                    Instance = HttpContext.Request.Path
                });
            }

            return NoContent();
        }
    }
}
