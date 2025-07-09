using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BarberAvailabilityUpdateDTO
    {
        public List<DayOfWeek> DayOfWeeks { get; set; } = new();
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int IntervalMinutes { get; set; }
    }
}
