namespace PODBookingSystem.Models
{
    public class PaymentDTO
    {
        public int BookingId { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; } 
    }
}
