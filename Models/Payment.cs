namespace PODBookingSystem.Models
{
    public class Payment
    {
        public int PaymentId { get; set; } // Khóa chính
        public int BookingId { get; set; } // Khóa ngoại tham chiếu đến Booking
        public double Amount { get; set; }
        public string PaymentMethod { get; set; } // Thẻ tín dụng, PayPal, Ví điện tử
        public string PaymentStatus { get; set; } // Đã thanh toán, Chờ thanh toán
    }
}
