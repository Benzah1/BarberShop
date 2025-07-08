﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ServiceDTO
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int DurationMinutes { get; set; }
        public int BarberId { get; set; }
    }
}
