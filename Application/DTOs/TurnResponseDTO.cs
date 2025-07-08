namespace Application.DTOs;

public class TurnoResponseDTO
{
    public int Id { get; set; }

    // Datos del usuario
    public string UserName { get; set; } = null!;
    public string UserEmail { get; set; } = null!;

    // Datos del Barbero
    public string BarberName { get; set; } = null!;

    // Datos del servicio
    public string ServiceName { get; set; } = null!;
    public decimal ServicePrice { get; set; }


    // Turno
    public DateTime TimeDate { get; set; }
    public bool Confirmed { get; set; }
}
