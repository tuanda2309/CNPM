using Microsoft.AspNetCore.Mvc.Rendering;

namespace PODBookingSystem.Models.DTOs
{
    public class BookingDTO
    {
        public int RoomId { get; set; } // ID của phòng
        public int UserId { get; set; } // ID của người dùng
        public DateTime StartTime { get; set; } // Thời gian bắt đầu
        public DateTime EndTime { get; set; } // Thời gian kết thúc
        public double TotalPrice { get; set; } // Tổng giá
        public string Status { get; set; } // Trạng thái
        public int? ServicePackageId { get; set; } // ID gói dịch vụ (nếu có)
        public bool IsHourly { get; set; } // Kiểu đặt phòng (theo giờ hay gói dịch vụ)
        public IEnumerable<SelectListItem> ServicePackageOptions { get; set; }
    }
}
