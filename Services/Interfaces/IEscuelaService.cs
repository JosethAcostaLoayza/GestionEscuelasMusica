// IEscuelaService.cs
using EscuelaMusica.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EscuelaMusica.API.Services.Interfaces
{
    public interface IEscuelaService
    {
        Task<List<Escuela>> GetAllAsync();
        Task<Escuela?> GetByIdAsync(int id);
        Task<Escuela> CreateAsync(Escuela escuela);
        Task<bool> UpdateAsync(Escuela escuela);
        Task<bool> DeleteAsync(int id);
    }
}
