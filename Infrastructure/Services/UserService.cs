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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {

        private readonly TurnoContext _context;

        public UserService(TurnoContext context)
        {
            _context = context;
        }

        public async Task<UserProfileDTO> GetProfile(int UserId)
        {

            var profile = await _context.Users.FindAsync(UserId);

            if (profile == null)
                throw new NotFoundException("Usuario no encontrado.");

            return new UserProfileDTO
            {
                Id = profile.Id,
                UserName = profile.UserName,
                UserEmail = profile.UserEmail,
                Role = profile.Role
            };

        }

        public async Task UpdateProfile(int UserId, UpdateUserDTO dto)
        {

            var profile = await _context.Users.FindAsync(UserId);
            if (profile == null)
                throw new NotFoundException("Usuario No Encontrado.");


            profile.UserName = dto.UserName;
            

            _context.Users.Update(profile);
            await _context.SaveChangesAsync();

        }


        public async Task ChangePassword(int UserId, ChangePasswordDTO dto)
        {
            var user = await _context.Users.FindAsync(UserId);
            if (user == null)
                throw new NotFoundException("Usuario No Encontrado.");

            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
                throw new BadRequestException("La contraseña actual es incorrecta");

            if (dto.NewPassword != dto.ConfirmNewPassword)
                throw new BadRequestException("Las contraseñas nuevas no coninciden");


            var newhash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.PasswordHash = newhash;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();




        }


    }
}
