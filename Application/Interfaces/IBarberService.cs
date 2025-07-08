using Application.DTOs;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBarberService
    {
        Task<List<Barber>> GetAllBarbers();
        Task<Barber> GetBarberById(int id);
        Task CreateBarber(BarberDTO dto);
        Task UpdateBarber(int id, BarberDTO dto);
        Task DeleteBarber(int id);

    }
}
