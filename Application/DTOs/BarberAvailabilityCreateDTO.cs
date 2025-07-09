using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BarberAvailabilityCreateDTO
    {
        public int BarberId { get; set; }
        public List<DayOfWeek> DayOfWeeks { get; set; } = new();
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int IntervalMinutes { get; set; }

    }
}
