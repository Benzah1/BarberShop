using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileDTO> GetProfile(int UserId);
        Task UpdateProfile(int UserId, UpdateUserDTO dto);
        Task ChangePassword(int UserId, ChangePasswordDTO dto);
    }
}
