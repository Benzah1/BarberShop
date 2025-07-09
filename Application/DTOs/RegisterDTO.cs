using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "El correo ingresado no tiene un formato valido")]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        
        public string Role { get; set; } = "cliente";


    }
}
