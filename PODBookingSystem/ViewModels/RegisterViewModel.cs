namespace PODBookingSystem.ViewModels
{
    public class RegisterViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; } = "Customer";
        public string? ProfileImage { get; set; }
        public string Title { get; set; } = "Chào mừng đến với hồ sơ của tôi 😉";
    }
}
