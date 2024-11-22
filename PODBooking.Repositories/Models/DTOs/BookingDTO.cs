using Microsoft.AspNetCore.Mvc.Rendering;

namespace PODBookingSystem.Models.DTOs
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public int RoomId { get; set; } 
        public int CustomerId { get; set; } 
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; } 
        public double TotalPrice { get; set; } 
        public int? ServicePackageId { get; set; } 
        public string Status { get; set; } = "Pending"; 
        public List<SelectListItem> ServicePackageOptions { get; set; } = new List<SelectListItem>();
        public string? RoomName { get; set; }
        public string? CustomerName { get; set; }
    }
}
