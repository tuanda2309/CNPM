using Microsoft.AspNetCore.Identity;

namespace PODBookingSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public string ProfileImage { get; set; }
        public string Title { get; set; }
    }
}