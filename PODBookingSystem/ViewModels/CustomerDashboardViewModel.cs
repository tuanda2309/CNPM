using PODBookingSystem.Models;

namespace PODBookingSystem.ViewModels
{
    public class CustomerDashboardViewModel
    {
        public string Username { get; set; }
        public List<BookingViewModel> UpcomingBookings { get; set; }
        public List<BookingViewModel> PastBookings { get; set; }
        public int RewardPoints { get; set; }
        public Customer CustomerDetails { get; set; }
    }
}
