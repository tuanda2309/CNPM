namespace PODBookingSystem.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }  

        public int UserId { get; set; } 
        public User User { get; set; }  
        public int? BookingId { get; set; }  
        public Booking Booking { get; set; }  
        public string Title { get; set; } 

        public string Message { get; set; }  

        public DateTime CreatedAt { get; set; } 

        public bool IsRead { get; set; }
    }
}
