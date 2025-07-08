using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ServiceResponseDTO
    {

        // service 
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int DurationMinutes { get; set; }

        // Datos del barbero
        public string BarberName { get; set; } = null!;
    }
}
