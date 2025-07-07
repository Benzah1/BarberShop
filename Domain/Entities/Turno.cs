namespace Domain.Entities;

public class Turno
{
    public int Id { get; set; }

    public string ClientName { get; set; } = null!;
    public string ClientEmail { get; set; } = null!;
    public string Barber { get; set; } = null!;
    public string Service { get; set; } = null!;
    public DateTime TimeDate { get; set; }
    public bool Confirmed { get; set; } = false;
}
