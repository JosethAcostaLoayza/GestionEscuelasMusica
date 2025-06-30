using Microsoft.AspNetCore.Mvc;
using EscuelaMusica.API.DTOs.Asignaciones;
using EscuelaMusica.API.Services.Interfaces;  // Aquí importas la interfaz de servicio

namespace EscuelaMusica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsignacionesController : ControllerBase
    {
        private readonly IAsignacionService _asignacionService;

        public AsignacionesController(IAsignacionService asignacionService)
        {
            _asignacionService = asignacionService;
        }

        // POST: api/Asignaciones/AsignarAlumnosProfesor
        [HttpPost("AsignarAlumnosProfesor")]
        public async Task<IActionResult> AsignarAlumnosAProfesor([FromBody] AsignacionAlumnoProfesorDTO dto)
        {
            if (dto.AlumnoIds == null || !dto.AlumnoIds.Any())
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Lista de alumnos vacía",
                    Status = 400,
                    Detail = "Debe proporcionar al menos un ID de alumno para asignar.",
                    Instance = HttpContext.Request.Path
                });
            }

            try
            {
                foreach (var alumnoId in dto.AlumnoIds)
                {
                    await _asignacionService.AsignarAlumnoAProfesorAsync(alumnoId, dto.ProfesorId);
                }
                return Ok(new
                {
                    Message = "Alumnos asignados al profesor correctamente.",
                    ProfesorId = dto.ProfesorId,
                    AlumnoIds = dto.AlumnoIds
                });
            }
            catch (InvalidOperationException invOpEx)
            {
                return Conflict(new ProblemDetails
                {
                    Title = "Alumno ya asignado",
                    Status = 409,
                    Detail = invOpEx.Message,
                    Instance = HttpContext.Request.Path
                });
            }
            catch (KeyNotFoundException knfEx)
            {
                // Si el servicio lanza excepciones específicas sobre IDs no encontrados
                return NotFound(new ProblemDetails
                {
                    Title = "Recurso no encontrado",
                    Status = 404,
                    Detail = knfEx.Message,
                    Instance = HttpContext.Request.Path
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Error al asignar alumnos",
                    Status = 400,
                    Detail = ex.Message,
                    Instance = HttpContext.Request.Path
                });
            }
        }

        // POST: api/Asignaciones/InscribirAlumnoEscuela
        [HttpPost("InscribirAlumnoEscuela")]
        public async Task<IActionResult> InscribirAlumnoAEscuela([FromBody] InscripcionAlumnoEscuelaDTO dto)
        {
            try
            {
                await _asignacionService.InscribirAlumnoAEscuelaAsync(dto.AlumnoId, dto.EscuelaId);
                return Ok(new
                {
                    Message = "Alumno inscrito en la escuela correctamente.",
                    AlumnoId = dto.AlumnoId,
                    EscuelaId = dto.EscuelaId
                });
            }
            catch (InvalidOperationException invOpEx)
            {
                return Conflict(new ProblemDetails
                {
                    Title = "Alumno ya inscrito",
                    Status = 409,
                    Detail = invOpEx.Message,
                    Instance = HttpContext.Request.Path
                });
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Recurso no encontrado",
                    Status = 404,
                    Detail = knfEx.Message,
                    Instance = HttpContext.Request.Path
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Error al inscribir alumno",
                    Status = 400,
                    Detail = ex.Message,
                    Instance = HttpContext.Request.Path
                });
            }
        }
    }
}
