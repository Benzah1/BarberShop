using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EmailDTO
    {
        public string To { get; set; } = null!;
        public string Subject { get; set; } = null!; 
        public string Body { get; set; } = null!;
    }
}
