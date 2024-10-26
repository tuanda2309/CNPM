namespace PODBookingSystem.Models
{
    public class Staff : User
    {
        public new string Role { get; set; } = "Staff";
        public bool IsActive { get; set; }
    }
}