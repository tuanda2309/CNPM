namespace PODBookingSystem.Models
{
    public class Customer : User
    {
        public bool IsVIP { get; set; } = false; 
        public new string Role { get; set; }
        public int Point { get; set; } = 0;
    }
}