// EscuelaService.cs
using EscuelaMusica.API.Data;
using EscuelaMusica.API.Entities;
using Microsoft.EntityFrameworkCore;
using EscuelaMusica.API.Services.Interfaces;
namespace EscuelaMusica.API.Services.Implementaciones
{
    public class EscuelaService : IEscuelaService
    {
        private readonly EscuelaMusicaContext _context;

        public EscuelaService(EscuelaMusicaContext context)
        {
            _context = context;
        }

        public async Task<List<Escuela>> GetAllAsync()
        {
            return await _context.Escuelas.ToListAsync();
        }

        public async Task<Escuela?> GetByIdAsync(int id)
        {
            return await _context.Escuelas.FindAsync(id);
        }

        public async Task<Escuela> CreateAsync(Escuela escuela)
        {
            // Validar código único
            if (await _context.Escuelas.AnyAsync(e => e.Codigo == escuela.Codigo))
                throw new System.Exception("Código de escuela ya existe.");

            _context.Escuelas.Add(escuela);
            await _context.SaveChangesAsync();
            return escuela;
        }

        public async Task<bool> UpdateAsync(Escuela escuela)
        {
            var existing = await _context.Escuelas.FindAsync(escuela.EscuelaId);
            if (existing == null)
                return false;

            // Verificar código único si cambió
            if (existing.Codigo != escuela.Codigo)
            {
                if (await _context.Escuelas.AnyAsync(e => e.Codigo == escuela.Codigo))
                    throw new System.Exception("Código de escuela ya existe.");
            }

            existing.Nombre = escuela.Nombre;
            existing.Descripcion = escuela.Descripcion;
            existing.Codigo = escuela.Codigo;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var escuela = await _context.Escuelas.FindAsync(id);
            if (escuela == null)
                return false;

            _context.Escuelas.Remove(escuela);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}