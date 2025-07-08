using Domain.Entities;

namespace Application.DTOs
{
    public class TurnoDTO
    {
        public int UserId { get; set; }
        public int BarberId { get; set; }
        public int ServiceId { get; set; }
        public DateTime TimeDate { get; set; }
        public bool Confirmed { get; set; } = false;
    }
}
