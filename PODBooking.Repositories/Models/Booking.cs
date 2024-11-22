namespace PODBookingSystem.Models
{
    public class Booking
    {
        public int BookingId { get; set; } 
        public int? UserId { get; set; } 
        public int RoomId { get; set; } 
        public DateTime BookingDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; } 
        public string PaymentStatus { get; set; }
    }
}
