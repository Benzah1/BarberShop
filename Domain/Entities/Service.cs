using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Service
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int DurationMinutes { get; set; }

        // Relación con Barber
        public int BarberId { get; set; }
        public Barber Barber { get; set; } = null!;
    }
}
