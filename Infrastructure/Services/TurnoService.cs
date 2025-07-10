using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions; 

namespace Infrastructure.Services;

public class TurnoService : ITurnoService
{
    private readonly TurnoContext _context;
    private readonly IEmailService _emailService;

    public TurnoService(TurnoContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<List<TurnoResponseDTO>> GetAllTurns()
    {
        return await _context.Turnos
            .Include(t => t.User)
            .Include(t => t.Barber)
            .Include(t => t.Service)
            .Select(t => new TurnoResponseDTO
            {
                Id = t.Id,
                UserName = t.User.UserName,
                UserEmail = t.User.UserEmail,
                BarberName = t.Barber.Name,
                ServiceName = t.Service.Name,
                ServicePrice = t.Service.Price,
                TimeDate = t.TimeDate,
                Confirmed = t.Confirmed
            })
            .ToListAsync();
    }

    public async Task<TurnoResponseDTO> GetTurnById(int id)
    {
        var turno = await _context.Turnos
            .Include(t => t.User) // Incluye los datos del usuario relacionado
            .Include(t => t.Barber)
            .Include(t => t.Service)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (turno == null)
            throw new NotFoundException("Turno no encontrado.");

        return new TurnoResponseDTO
        {
            Id = turno.Id,
            UserName = turno.User.UserName,
            UserEmail = turno.User.UserEmail,
            BarberName = turno.Barber.Name,
            ServiceName = turno.Service.Name,
            ServicePrice = turno.Service.Price,
            TimeDate = turno.TimeDate,
            Confirmed = turno.Confirmed
        };
    }


    public async Task CreateTurn(TurnoDTO dto)
    {
        var exists = await _context.Turnos.AnyAsync(t =>
            t.BarberId == dto.BarberId && t.TimeDate == dto.TimeDate);

        if (exists)
            throw new BadRequestException("Esta Hora ya fue tomada. Por favor elija otra");

        // Obtener datos adicionales del barbero y servicio
        var barber = await _context.Barbers.FindAsync(dto.BarberId);
        var service = await _context.Services.FindAsync(dto.ServiceId);
        var user = await _context.Users.FindAsync(dto.UserId);

        if (barber == null || service == null || user == null)
            throw new NotFoundException("Datos inválidos al crear el turno");

        var turno = new Turno
        {
            UserId = dto.UserId,
            BarberId = dto.BarberId,
            ServiceId = dto.ServiceId,
            TimeDate = dto.TimeDate,
            Confirmed = false
        };

        _context.Turnos.Add(turno);
        await _context.SaveChangesAsync();

        // Enlaces de confirmación y cancelación simulados
        var confirmationLink = $"http://localhost:5010/api/turnos/confirm?UserId={dto.UserId}&BarberId={dto.BarberId}&time={dto.TimeDate:O}";
        var cancellationLink = $"http://localhost:5010/api/turnos/Canceling?UserId={dto.UserId}&BarberId={dto.BarberId}&time={dto.TimeDate:O}";

        var subject = "📅 Confirmación de hora - BarberShop";
        var body = $@"
        <h2>¡Hola {user.UserName}!</h2>
        <p>Gracias por reservar con <strong>BarberShop</strong>.</p>
        <p><strong>Tu cita:</strong></p>
        <ul>
            <li><strong>Barbero:</strong> {barber.Name}</li>
            <li><strong>Servicio:</strong> {service.Name}</li>
            <li><strong>Precio:</strong> ${service.Price}</li>
            <li><strong>Fecha y hora:</strong> {dto.TimeDate:dddd dd MMMM yyyy HH:mm}</li>
        </ul>
        <p>
            ✅ <a href='{confirmationLink}'>Confirmar</a> &nbsp;&nbsp;
            ❌ <a href='{cancellationLink}'>Cancelar</a>
        </p>
        <hr/>
        <p style='font-size:small;'>Equipo BarberShop</p>
        ";

        await _emailService.SendEmail(new EmailDTO
        {
            To = user.UserEmail,
            Subject = subject,
            Body = body
        });
    }


    public async Task UpdateTurn(int id, TurnoDTO dto)
    {
        var turno = await _context.Turnos.FindAsync(id);
        if (turno == null)
            throw new NotFoundException("Turno No Encontrado.");

        turno.UserId = dto.UserId;
        turno.BarberId = dto.BarberId;
        turno.ServiceId = dto.ServiceId;
        turno.TimeDate = dto.TimeDate;
        turno.Confirmed = dto.Confirmed;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteTurn(int id)
    {
        var turno = await _context.Turnos.FindAsync(id);
        if (turno == null)
            throw new NotFoundException("Turno No Encontrado.");

        _context.Turnos.Remove(turno);
        await _context.SaveChangesAsync();
    }


    public async Task ConfirmTime(int UserId, int BarberId, DateTime time)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);

        if (user == null)
            throw new NotFoundException("Usuario no encontrado");

        var turn = await _context.Turnos.FirstOrDefaultAsync(t => t.BarberId == BarberId && t.TimeDate == time);

        if (turn == null)
            throw new NotFoundException("Turno no encontrado");

        if (turn.Confirmed)
            throw new BadRequestException("Esta hora ya fue confirmada");

        turn.Confirmed = true;

        await _context.SaveChangesAsync();
    }

    public async Task CancelTime(int UserId, int BarberId, DateTime time)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);

        if (user == null)
            throw new NotFoundException("Usuario no encontrado");

        var turn = await _context.Turnos.FirstOrDefaultAsync(t => t.BarberId == BarberId && t.TimeDate == time);

        if (turn == null)
            throw new NotFoundException("Turno no encontrado");


        _context.Turnos.Remove(turn);
        await _context.SaveChangesAsync();
    }


    public async Task<List<TurnoResponseDTO>> GetAllTurnsBarber(int barberid)
    {

        var now = DateTime.UtcNow;

        return await _context.Turnos
            .Include(t => t.User)
            .Include(t => t.Barber)
            .Include(t => t.Service)
            .Where(t => t.BarberId == barberid && t.TimeDate >= now)
            .Select(t => new TurnoResponseDTO
            {
                Id = t.Id,
                UserName = t.User.UserName,
                UserEmail = t.User.UserEmail,
                BarberName = t.Barber.Name,
                ServiceName = t.Service.Name,
                ServicePrice = t.Service.Price,
                TimeDate = t.TimeDate,
                Confirmed = t.Confirmed
            })
            .ToListAsync();
    }

}
