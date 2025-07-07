using Application.DTOs;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface ITurnoService
    {
        // Obtener todos los turnos
        Task<List<Turno>> GetAllTurns();

        // Obtener un turno por su ID
        Task<Turno?> GetTurnById(int id);

        // Crear un nuevo turno
        Task CreateTurn(TurnoDTO dto);

        // Actualizar un turno existente
        Task UpdateTurn(int id, TurnoDTO dto);

        // Eliminar un turno por su ID
        Task DeleteTurn(int id);
    }
}
