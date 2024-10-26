namespace PODBookingSystem.Models.DTOs
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; } // CreditCard, PayPal, E-Wallet
        public string PaymentStatus { get; set; } // Paid, Pending
        public DateTime PaymentDate { get; set; }
    }

}
