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

    public async Task<List<Turno>> GetAllTurns()
    {
        return await _context.Turnos.ToListAsync();
    }

    public async Task<Turno?> GetTurnById(int id)
    {
        var turno = await _context.Turnos.FindAsync(id);
        if (turno == null)
            throw new NotFoundException("Turno No Encontrado.");

        return turno;
    }

    public async Task CreateTurn(TurnoDTO dto)
    {
        var exists = await _context.Turnos.AnyAsync(t =>
            t.Barber == dto.Barber && t.TimeDate == dto.TimeDate);

        if (exists)
            throw new BadRequestException("Esta Hora ya fue tomada. Por favor elija otra");

        var turno = new Turno
        {
            ClientName = dto.ClientName,
            ClientEmail = dto.ClientEmail,
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

        turno.ClientName = dto.ClientName;
        turno.ClientEmail = dto.ClientEmail;
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
