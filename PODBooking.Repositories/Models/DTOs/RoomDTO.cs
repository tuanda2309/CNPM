namespace PODBookingSystem.Models.DTOs
{
    public class RoomDTO
    {
        public int RoomId { get; set; }  
        public string Name { get; set; }  
        public string Description { get; set; }  
        public int Capacity { get; set; }  
        public double Price { get; set; }  
        public DateTime AvailableFrom { get; set; }  
        public DateTime AvailableTo { get; set; }  
    }
}
