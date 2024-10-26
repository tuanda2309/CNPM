using System.ComponentModel.DataAnnotations;

namespace PODBookingSystem.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        

        [Required]
        public double Price { get; set; }

        public int? UserId { get; set; } // Nullable nếu phòng không liên kết với người dùng nào

        public string Location { get; set; } // Địa điểm của phòng

        public DateTime DatePosted { get; set; } = DateTime.Now;
        public string OwnerName { get; set; }

        
        public string CreatedBy { get; set; }
        
        public string Image { get; set; }
        public int Capacity { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public bool IsAvailable { get; set; }
    }
}
