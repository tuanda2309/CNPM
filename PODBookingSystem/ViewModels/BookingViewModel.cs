namespace PODBookingSystem.ViewModels
{
    public class BookingViewModel
    {
        public DateTime BookingDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string RoomName { get; set; }
        public string Status { get; set; }
        public int RoomId { get; set; }
    }

}
