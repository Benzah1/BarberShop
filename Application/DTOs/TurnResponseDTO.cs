namespace Application.DTOs;

public class TurnoResponseDTO
{
    public int Id { get; set; }

    // Datos del usuario
    public string UserName { get; set; } = null!;
    public string UserEmail { get; set; } = null!;

    // Datos del Barber
    public string BarberName { get; set; } = null!;

    // Turno
    public string Service { get; set; } = null!;
    public DateTime TimeDate { get; set; }
    public bool Confirmed { get; set; }
}
