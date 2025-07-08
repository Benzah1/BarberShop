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
    public class BarberService : IBarberService
    {
        private readonly TurnoContext _context;

        public BarberService(TurnoContext context)
        {
            _context = context;
        }


        public async Task<List<Barber>> GetAllBarbers()
        {
            return await _context.Barbers.ToListAsync();
        }

        public async Task<Barber> GetBarberById(int id)
        {
            var barber = await _context.Barbers.FindAsync(id);

            if (barber == null)
                throw new NotFoundException("Barbero no encontrado.");

            return barber;
            
        } 
         
        public async Task CreateBarber(BarberDTO dto)
        {

            var exists = await _context.Barbers.AnyAsync(b => b.Email == dto.Email);
            if (exists)
                throw new BadRequestException("El barbero ya existe intenta con otro correo");

            var barber = new Barber
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                IsActive = dto.IsActive
            };

            _context.Barbers.Add(barber);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateBarber(int id, BarberDTO dto)
        {
            var barber = await _context.Barbers.FindAsync(id);
            if (barber == null)
                throw new NotFoundException("Barbero No Encontrado.");

            barber.Name = dto.Name;
            barber.Email = dto.Email;
            barber.Phone = dto.Phone;
            barber.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBarber(int id)
        {
            var barber = await _context.Barbers.FindAsync(id);
            if (barber == null)
                throw new NotFoundException("Barbero No Encontrado.");

            _context.Barbers.Remove(barber);
            await _context.SaveChangesAsync();
        }

    }
}
