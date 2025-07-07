namespace Application.DTOs
{
    public class TurnoDTO
    {
        public string ClientName { get; set; } = null!;
        public string ClientEmail { get; set; } = null!;
        public string Barber { get; set; } = null!;
        public string Service { get; set; } = null!;
        public DateTime TimeDate { get; set; }
        public bool Confirmed { get; set; } = false;
    }
}
