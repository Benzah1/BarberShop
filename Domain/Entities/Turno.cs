namespace Domain.Entities;

public class Turno
{
    public int Id { get; set; }

    public int UserId { get; set; }           
    public User User { get; set; } = null!;
    public string Barber { get; set; } = null!;
    public string Service { get; set; } = null!;
    public DateTime TimeDate { get; set; }
    public bool Confirmed { get; set; } = false;
}
