using PODBookingSystem.Models;

namespace PODBookingSystem.ViewModels
{
    public class MakeBookingViewModel
    {
        public int SelectedPODId { get; set; }
        public int SelectedServicePackageId { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<string> AvailablePODs { get; set; }
        public List<string> SelectedServices { get; set; }

    }
}
