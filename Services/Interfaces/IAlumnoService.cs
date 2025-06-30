// IAlumnoService.cs
using EscuelaMusica.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EscuelaMusica.API.Services.Interfaces
{
    public interface IAlumnoService
    {
        Task<List<Alumno>> GetAllAsync();
        Task<Alumno?> GetByIdAsync(int id);
        Task<Alumno> CreateAsync(Alumno alumno);
        Task<bool> UpdateAsync(Alumno alumno);
        Task<bool> DeleteAsync(int id);
    }
}
