using PODBookingSystem.Models;

namespace PODBookingSystem.ViewModels
{
    public class StaffDashboardViewModel
    {
        public IEnumerable<Booking> Bookings { get; set; }
        public int TotalBookings { get; set; }
       
    }
}
