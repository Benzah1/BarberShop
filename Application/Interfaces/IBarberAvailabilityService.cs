using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBarberAvailabilityService
    {
        // Crear una nueva regla de disponibilidad
        Task CreateAvailabilityRule(BarberAvailabilityCreateDTO dto);

        // Obtener todas las reglas de disponibilidad de un barbero
        Task<List<BarberAvailabilityResponseDTO>> GetAvailabilityByBarber(int barberId);

        // Actualizar una regla existente
        Task UpdateAvailabilityRule(int ruleId, BarberAvailabilityUpdateDTO dto);

        // Eliminar una regla de disponibilidad
        Task DeleteAvailabilityRule(int ruleId);

        Task<List<DateTime>> GetAvailableTimeSlots(int barberId, DateTime date);
        

    }
}
