namespace PODBookingSystem.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
    }
}