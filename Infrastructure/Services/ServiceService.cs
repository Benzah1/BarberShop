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
    public class ServiceService : IServiceService
    {
        private readonly TurnoContext _context;

        public ServiceService(TurnoContext context)
        {
            _context = context;
        }


        public async Task<List<ServiceResponseDTO>> GetServicesByBarber(int barberId)
        {
            var barberExists = await _context.Barbers.AnyAsync(b => b.Id == barberId);
            if (!barberExists)
                throw new NotFoundException("Barbero no encontrado.");

            var servicios = await _context.Services
                .Where(s => s.BarberId == barberId)
                .Select(s => new ServiceResponseDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    DurationMinutes = s.DurationMinutes,
                    BarberName = s.Barber.Name
                })
                .ToListAsync();

            if (servicios.Count == 0)
                throw new NotFoundException("Este barbero no tiene servicios registrados.");

            return servicios;
        }

        public async Task CreateService(ServiceDTO dto)
        {
            var barberExists = await _context.Barbers.AnyAsync(b => b.Id == dto.BarberId);
            if (!barberExists)
                throw new NotFoundException("Barbero no encontrado.");

            var exists = await _context.Services.AnyAsync(s => s.BarberId == dto.BarberId && s.Name == dto.Name);
            if (exists)
                throw new BadRequestException("Este servicio ya existe para este barbero.");

            var service = new Service
            {
                Name = dto.Name,
                Price = dto.Price,
                DurationMinutes = dto.DurationMinutes,
                BarberId = dto.BarberId
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateService(int id, ServiceDTO dto)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                throw new BadRequestException("Este servicio no existe.");

            var barberExists = await _context.Barbers.AnyAsync(b => b.Id == dto.BarberId);
            if (!barberExists)
                throw new NotFoundException("Barbero no encontrado.");


            service.Name = dto.Name;
            service.Price = dto.Price;
            service.DurationMinutes = dto.DurationMinutes;
            service.BarberId = dto.BarberId;

            await _context.SaveChangesAsync();

        }
        public async Task DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                throw new NotFoundException("Servicio no encontrado.");

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }
    }
}
