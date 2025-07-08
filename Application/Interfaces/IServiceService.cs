using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServiceService
    {

        // Obtener todos los servicios de un barbero
        Task<List<ServiceResponseDTO>> GetServicesByBarber(int barberId);

        // Crear un nuevo servicio para un barbero
        Task CreateService(ServiceDTO dto);

        // Actualizar un servicio existente
        Task UpdateService(int id, ServiceDTO dto);

        // Eliminar un servicio
        Task DeleteService(int id);
    }
}

