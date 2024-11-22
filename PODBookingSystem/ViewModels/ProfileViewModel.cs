using PODBookingSystem.Models;

namespace PODBookingSystem.ViewModels
{
    public class ProfileViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Image { get; set; } 
        public IFormFile ImageFile { get; set; }  
        public string Title { get; set; }
        public string Role { get; set; }
        public List<Room> BookedRooms { get; set; } = new List<Room>();
    }
}
