using System;

namespace PODBookingSystem.Models
{
    /* class Booking
    {
        public int Id { get; set; } // ID của đặt chỗ
        public string Username { get; set; } // Tên người dùng
        public DateTime BookingDate { get; set; } // Ngày đặt
        public int PODId { get; set; } // ID của phòng POD
        public int ServicePackageId { get; set; } // ID của gói dịch vụ
        public virtual ServicePackage ServicePackage { get; set; }
        public double Price { get; set; } // Giá đặt phòng
        public DateTime StartDateTime { get; set; } // Thời gian bắt đầu
        public DateTime EndDateTime { get; set; } // Thời gian kết thúc
        public string Status { get; set; } // Trạng thái đặt phòng (Pending, Confirmed, Cancelled)
        public int CustomerId { get; set; } // ID khách hàng
        public virtual Customer Customer { get; set; } // Thông tin khách hàng
        public bool IsServiced { get; set; } // Đã cung cấp dịch vụ chưa
        public string AdditionalServices { get; set; } // Tiện ích đi kèm
    }*/
    public class Booking
    {
        public int BookingId { get; set; } // Khóa chính
        public int UserId { get; set; } // Khóa ngoại tham chiếu đến User
        public int RoomId { get; set; } // Khóa ngoại tham chiếu đến Room
        public int ServicePackageId { get; set; } // Khóa ngoại tham chiếu đến ServicePackage
        public DateTime BookingDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; } // Đang chờ, Đã xác nhận, Đã hủy
        //public virtual Room Room { get; set; }
        //public virtual ServicePackage ServicePackage { get; set; }
        //public virtual User User { get; set; }
    }
}
