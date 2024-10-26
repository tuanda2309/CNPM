using System;

namespace PODBookingSystem.Models.DTOs
{
    public class RoomDTO
    {
        public int RoomId { get; set; }  // ID của phòng
        public string Name { get; set; }  // Tên của phòng
        public string Description { get; set; }  // Mô tả phòng
        public int Capacity { get; set; }  // Sức chứa của phòng
        public double Price { get; set; }  // Giá của phòng
        public DateTime AvailableFrom { get; set; }  // Thời gian có sẵn từ
        public DateTime AvailableTo { get; set; }  // Thời gian có sẵn đến
    }
}
