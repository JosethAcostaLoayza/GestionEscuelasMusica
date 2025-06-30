// IAsignacionService.cs
using System.Threading.Tasks;
namespace EscuelaMusica.API.Services.Interfaces
{
    public interface IAsignacionService
    {
        Task<bool> AsignarAlumnoAProfesorAsync(int alumnoId, int profesorId);
        Task<bool> InscribirAlumnoAEscuelaAsync(int alumnoId, int escuelaId);
    }
}
