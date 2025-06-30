// ProfesorService.cs
using EscuelaMusica.API.Data;
using EscuelaMusica.API.Entities;
using Microsoft.EntityFrameworkCore;
using EscuelaMusica.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EscuelaMusica.API.Services.Implementaciones
{
    public class ProfesorService : IProfesorService
    {
        private readonly EscuelaMusicaContext _context;

        public ProfesorService(EscuelaMusicaContext context)
        {
            _context = context;
        }

        public async Task<List<Profesor>> GetAllAsync()
        {
            return await _context.Profesores.Include(p => p.Escuela).ToListAsync();
        }

        public async Task<Profesor?> GetByIdAsync(int id)
        {
            return await _context.Profesores.Include(p => p.Escuela)
                                            .FirstOrDefaultAsync(p => p.ProfesorId == id);
        }

        public async Task<Profesor> CreateAsync(Profesor profesor)
        {
            // Validar identificacion única
            if (await _context.Profesores.AnyAsync(p => p.Identificacion == profesor.Identificacion))
                throw new System.Exception("Identificación de profesor ya existe.");

            _context.Profesores.Add(profesor);
            await _context.SaveChangesAsync();
            return profesor;
        }

        public async Task<bool> UpdateAsync(Profesor profesor)
        {
            var existing = await _context.Profesores.FindAsync(profesor.ProfesorId);
            if (existing == null)
                return false;

            if (existing.Identificacion != profesor.Identificacion)
            {
                if (await _context.Profesores.AnyAsync(p => p.Identificacion == profesor.Identificacion))
                    throw new System.Exception("Identificación de profesor ya existe.");
            }

            existing.Nombre = profesor.Nombre;
            existing.Apellido = profesor.Apellido;
            existing.Identificacion = profesor.Identificacion;
            existing.EscuelaId = profesor.EscuelaId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
                return false;

            _context.Profesores.Remove(profesor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}