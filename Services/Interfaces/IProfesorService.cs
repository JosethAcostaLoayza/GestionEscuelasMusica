// IProfesorService.cs
using EscuelaMusica.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EscuelaMusica.API.Services.Interfaces
{
    public interface IProfesorService
    {
        Task<List<Profesor>> GetAllAsync();
        Task<Profesor?> GetByIdAsync(int id);
        Task<Profesor> CreateAsync(Profesor profesor);
        Task<bool> UpdateAsync(Profesor profesor);
        Task<bool> DeleteAsync(int id);
    }
}
