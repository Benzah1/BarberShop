using Domain.Entities;

public class BarberAvailabilityRule
{
    public int Id { get; set; }
    public int BarberId { get; set; }
    public Barber Barber { get; set; } = null!;
    public List<BarberAvailabilityDay> Days { get; set; } = new();
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int IntervalMinutes { get; set; }
}
