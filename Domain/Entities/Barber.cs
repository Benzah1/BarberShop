using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Barber
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool IsActive { get; set; } = true;

        // Relación con Turnos
        public List<Turno> Turnos { get; set; } = new();
        //Relacion con services
        public List<Service> Services { get; set; } = new();


    }
}
