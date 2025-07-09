using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "El correo ingresado no tiene un formato valido")]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

    }
}
