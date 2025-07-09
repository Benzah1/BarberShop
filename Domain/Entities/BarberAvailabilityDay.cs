using Domain.Entities;

public class BarberAvailabilityDay
{
    public int Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }

    // Clave foránea y navegación correctamente nombradas
    public int AvailabilityRuleId { get; set; }
    public BarberAvailabilityRule AvailabilityRule { get; set; } = null!;
}
