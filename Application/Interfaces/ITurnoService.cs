using Application.DTOs;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITurnoService
    {
        // Obtener todos los turnos
        Task<List<TurnoResponseDTO>> GetAllTurns();

        // Obtener un turno por su ID
        Task<TurnoResponseDTO> GetTurnById(int id);

        // Crear un nuevo turno
        Task CreateTurn(TurnoDTO dto);

        // Actualizar un turno existente
        Task UpdateTurn(int id, TurnoDTO dto);

        // Eliminar un turno por su ID
        Task DeleteTurn(int id);

        Task ConfirmTime(int UserId, int BarberId, DateTime time);

        Task CancelTime(int UserId, int BarberId, DateTime time);
        Task<List<TurnoResponseDTO>> GetAllTurnsBarber(int barberid);

    }
}
