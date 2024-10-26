namespace PODBookingSystem.Models
{
    public class Report
    {
        public int ReportId { get; set; } // Khóa chính
        public int UserId { get; set; } // Khóa ngoại tham chiếu đến User
        public int BookingId { get; set; } // Khóa ngoại tham chiếu đến Booking
        public string ReportType { get; set; } // Doanh thu, Sử dụng
        public string Content { get; set; } // Nội dung chi tiết của báo cáo
    }
}
