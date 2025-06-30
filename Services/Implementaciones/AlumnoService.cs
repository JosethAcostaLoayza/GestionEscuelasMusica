// AlumnoService.cs
using EscuelaMusica.API.Data;
using EscuelaMusica.API.Entities;
using Microsoft.EntityFrameworkCore;
using EscuelaMusica.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EscuelaMusica.API.Services.Implementaciones
{
    public class AlumnoService : IAlumnoService
    {
        private readonly EscuelaMusicaContext _context;

        public AlumnoService(EscuelaMusicaContext context)
        {
            _context = context;
        }

        public async Task<List<Alumno>> GetAllAsync()
        {
            return await _context.Alumnos.ToListAsync();
        }

        public async Task<Alumno?> GetByIdAsync(int id)
        {
            return await _context.Alumnos.FindAsync(id);
        }

        public async Task<Alumno> CreateAsync(Alumno alumno)
        {
            // Validar identificacion única
            if (await _context.Alumnos.AnyAsync(a => a.Identificacion == alumno.Identificacion))
                throw new System.Exception("Identificación de alumno ya existe.");

            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();
            return alumno;
        }

        public async Task<bool> UpdateAsync(Alumno alumno)
        {
            var existing = await _context.Alumnos.FindAsync(alumno.AlumnoId);
            if (existing == null)
                return false;

            if (existing.Identificacion != alumno.Identificacion)
            {
                if (await _context.Alumnos.AnyAsync(a => a.Identificacion == alumno.Identificacion))
                    throw new System.Exception("Identificación de alumno ya existe.");
            }

            existing.Nombre = alumno.Nombre;
            existing.Apellido = alumno.Apellido;
            existing.FechaNacimiento = alumno.FechaNacimiento;
            existing.Identificacion = alumno.Identificacion;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
                return false;

            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}