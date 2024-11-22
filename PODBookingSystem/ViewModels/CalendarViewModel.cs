
using PODBookingSystem.Models.DTOs;

namespace PODBookingSystem.ViewModels
{
    public class CalendarViewModel
    {
        public string Name { get; set; }
        public List<BookingDTO> Bookings { get; set; }
        public DateTime CurrentWeekStart { get; set; }
        public DateTime CurrentWeekEnd { get; set; }
    }
}
