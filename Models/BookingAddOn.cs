namespace PODBookingSystem.Models
{
    public class BookingAddOn
    {
        public int BookingAddOnId { get; set; } // Khóa chính
        public int BookingId { get; set; } // Khóa ngoại tham chiếu đến Booking
        public int ServiceAddOnId { get; set; } // Khóa ngoại tham chiếu đến ServiceAddOn
        public int Quantity { get; set; } // Số lượng tiện ích thêm
    }
}
