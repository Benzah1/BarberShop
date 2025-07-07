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

    public TurnoService(TurnoContext context)
    {
        _context = context;
    }

    public async Task<List<TurnoResponseDTO>> GetAllTurns()
    {
        return await _context.Turnos
            .Include(t => t.User)
            .Select(t => new TurnoResponseDTO
            {
                Id = t.Id,
                UserName = t.User.UserName,
                UserEmail = t.User.UserEmail,
                Barber = t.Barber,
                Service = t.Service,
                TimeDate = t.TimeDate,
                Confirmed = t.Confirmed
            })
            .ToListAsync();
    }

    public async Task<TurnoResponseDTO> GetTurnById(int id)
    {
        var turno = await _context.Turnos
            .Include(t => t.User) // Incluye los datos del usuario relacionado
            .FirstOrDefaultAsync(t => t.Id == id);

        if (turno == null)
            throw new NotFoundException("Turno no encontrado.");

        return new TurnoResponseDTO
        {
            Id = turno.Id,
            UserName = turno.User.UserName,
            UserEmail = turno.User.UserEmail,
            Barber = turno.Barber,
            Service = turno.Service,
            TimeDate = turno.TimeDate,
            Confirmed = turno.Confirmed
        };
    }


    public async Task CreateTurn(TurnoDTO dto)
    {
        var exists = await _context.Turnos.AnyAsync(t =>
            t.Barber == dto.Barber && t.TimeDate == dto.TimeDate);

        if (exists)
            throw new BadRequestException("Esta Hora ya fue tomada. Por favor elija otra");

        var turno = new Turno
        {
           UserId = dto.UserId,
            Barber = dto.Barber,
            Service = dto.Service,
            TimeDate = dto.TimeDate,
            Confirmed = dto.Confirmed
        };

        _context.Turnos.Add(turno);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTurn(int id, TurnoDTO dto)
    {
        var turno = await _context.Turnos.FindAsync(id);
        if (turno == null)
            throw new NotFoundException("Turno No Encontrado.");

        turno.UserId = dto.UserId;
        turno.Barber = dto.Barber;
        turno.Service = dto.Service;
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
}
