using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateUserDTO
    {
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;

    }
}
