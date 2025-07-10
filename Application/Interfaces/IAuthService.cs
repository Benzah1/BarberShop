using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task RegisterUser(RegisterDTO dto);

        Task<string> LoginUser(LoginDTO dto);
        Task ConfirmEmail(string email, string code);
        Task ResendVerificationCode(string email);
    }
}
