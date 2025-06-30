// AsignacionService.cs
using EscuelaMusica.API.Data;
using EscuelaMusica.API.Entities;
using Microsoft.EntityFrameworkCore;
using EscuelaMusica.API.Services.Interfaces;
using System.Threading.Tasks;
namespace EscuelaMusica.API.Services.Implementaciones
{
    public class AsignacionService : IAsignacionService
    {
        private readonly EscuelaMusicaContext _context;

        public AsignacionService(EscuelaMusicaContext context)
        {
            _context = context;
        }

        public async Task<bool> AsignarAlumnoAProfesorAsync(int alumnoId, int profesorId)
        {
            var existe = await _context.AlumnoProfesores
                .AnyAsync(ap => ap.AlumnoId == alumnoId && ap.ProfesorId == profesorId);
            if (existe) throw new Exception("El alumno ya está asignado a ese profesor.");

            _context.AlumnoProfesores.Add(new AlumnoProfesor
            {
                AlumnoId = alumnoId,
                ProfesorId = profesorId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InscribirAlumnoAEscuelaAsync(int alumnoId, int escuelaId)
        {
            var existe = await _context.AlumnoEscuelas
                .AnyAsync(ae => ae.AlumnoId == alumnoId && ae.EscuelaId == escuelaId);
            if (existe) throw new Exception("El alumno ya está inscrito en esa escuela.");

            _context.AlumnoEscuelas.Add(new AlumnoEscuela
            {
                AlumnoId = alumnoId,
                EscuelaId = escuelaId
            });

            await _context.SaveChangesAsync();
            return true;
        }
    }
}