using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BarberAvailabilityResponseDTO
    {
        public int RuleId { get; set; }
        public List<string> Days { get; set; } = new();
        public string StartTime { get; set; } = null!;
        public string EndTime { get; set; } = null!;
        public int IntervalMinutes { get; set; }
    }
}
