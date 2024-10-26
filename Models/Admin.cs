namespace PODBookingSystem.Models
{
    public class Admin : User
    {
        public new string Role { get; set; } = "Admin";
    }
}
