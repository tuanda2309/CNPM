using System.ComponentModel.DataAnnotations;

namespace PODBookingSystem.Models
{
    public class ServicePackage
    {
        public int ServicePackageId { get; set; }  // ID của gói dịch vụ
        public string Name { get; set; }             // Tên của gói dịch vụ
        public string Description { get; set; }      // Mô tả về gói dịch vụ
        public double Price { get; set; }            // Giá của gói dịch vụ
        public DateTime CreatedAt { get; set; }
    }
}
