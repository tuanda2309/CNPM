namespace PODBookingSystem.Models
{
    public class ServiceAddOn
    {
        public int ServiceAddOnId { get; set; } // Khóa chính
        public string Name { get; set; } // Ví dụ: Đồ uống, In ấn, Phòng họp
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image {  get; set; }
    }
}
