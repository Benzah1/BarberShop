using Domain.Entities;

namespace Application.DTOs
{
    public class TurnoDTO
    {
        public int UserId { get; set; }
        public string Barber { get; set; } = null!;
        public string Service { get; set; } = null!;
        public DateTime TimeDate { get; set; }
        public bool Confirmed { get; set; } = false;
    }
}
