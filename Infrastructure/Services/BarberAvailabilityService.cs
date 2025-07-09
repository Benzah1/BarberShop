using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services 
{
    public class BarberAvailabilityService :IBarberAvailabilityService
    {
        private readonly TurnoContext _context;

        public BarberAvailabilityService(TurnoContext context)
        {
            _context = context;
        }


        public async Task CreateAvailabilityRule(BarberAvailabilityCreateDTO dto)
        {
            var barberExists = await _context.Barbers.AnyAsync(b => b.Id == dto.BarberId);
            if (!barberExists)
                throw new NotFoundException("El barbero no existe");

            foreach(var day in dto.DayOfWeeks)
            {
                var ruleExists = await _context.Rules
                    .Include(r => r.Days)
                    .AnyAsync(r => r.BarberId == dto.BarberId && r.Days.Any(d => d.DayOfWeek == day));

                if (ruleExists)
                    throw new BadRequestException($"Ya existe una regla para el dia {day}");

                var rule = new BarberAvailabilityRule
                {
                    BarberId = dto.BarberId,
                    Days = new List<BarberAvailabilityDay>
                    {
                        new BarberAvailabilityDay {DayOfWeek = day}
                    },
                    StartTime = dto.StartTime,
                    EndTime = dto.EndTime,
                    IntervalMinutes = dto.IntervalMinutes
                };

                _context.Rules.Add(rule);

            }

            await _context.SaveChangesAsync();

        }

        public async Task<List<BarberAvailabilityResponseDTO>> GetAvailabilityByBarber(int barberId)
        {
            var barberExists = await _context.Barbers.FindAsync(barberId);
            if (barberExists == null)
                throw new NotFoundException("El barbero no existe");


            var rules = await _context.Rules
                .Include(r => r.Days)
                .Where(r => r.BarberId == barberId)
                .Select(r => new BarberAvailabilityResponseDTO
                {
                    RuleId = r.Id,
                    Days = r.Days.Select(d => d.DayOfWeek.ToString()).ToList(),
                    StartTime = r.StartTime.ToString(@"hh\:mm"),
                    EndTime = r.EndTime.ToString(@"hh\:mm"),
                    IntervalMinutes = r.IntervalMinutes
                }) 
                .ToListAsync();

            if (rules.Count == 0)
                throw new NotFoundException("Este Barbero no tiene nunguna regla registrada");

            return rules;
               
        }

        public async Task UpdateAvailabilityRule(int ruleId, BarberAvailabilityUpdateDTO dto)
        {
            var rule = await _context.Rules
                .Include(r => r.Days)
                .FirstOrDefaultAsync(r => r.Id == ruleId);

            if (rule == null)
                throw new NotFoundException("Regla de disponibilidad no encontrada.");

            // Reemplaza los días
            rule.Days = dto.DayOfWeeks.Select(day => new BarberAvailabilityDay
            {
                DayOfWeek = day
            }).ToList();

            // Actualiza otros campos
            rule.StartTime = dto.StartTime;
            rule.EndTime = dto.EndTime;
            rule.IntervalMinutes = dto.IntervalMinutes;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAvailabilityRule(int ruleId)
        {
            var rule = await _context.Rules.FindAsync(ruleId);
            if (rule == null)
                throw new NotFoundException("Regla de disponibilidad no encontrada.");

            _context.Rules.Remove(rule);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DateTime>> GetAvailableTimeSlots(int barberId, DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;

            // 1. Obtener reglas de disponibilidad para ese día
            var rules = await _context.Rules
                .Include(r => r.Days)
                .Where(r => r.BarberId == barberId && r.Days.Any(d => d.DayOfWeek == dayOfWeek))
                .ToListAsync();

            if (!rules.Any())
                return new List<DateTime>(); // No hay reglas para ese día

            // 2. Generar todos los slots posibles según las reglas
            var possibleSlots = new List<DateTime>();
            foreach (var rule in rules)
            {
                var currentTime = rule.StartTime;
                while (currentTime + TimeSpan.FromMinutes(rule.IntervalMinutes) <= rule.EndTime)
                {
                    possibleSlots.Add(date.Date + currentTime);
                    currentTime += TimeSpan.FromMinutes(rule.IntervalMinutes);
                }
            }

            // 3. Obtener los turnos ya reservados para ese día
            var reservedSlots = await _context.Turnos
                .Where(t => t.BarberId == barberId && t.TimeDate.Date == date.Date)
                .Select(t => t.TimeDate)
                .ToListAsync();

            // 4. Filtrar solo los horarios que NO están reservados
            var availableSlots = possibleSlots
                .Where(slot => !reservedSlots.Contains(slot))
                .ToList();

            return availableSlots;
        }






    }
}
