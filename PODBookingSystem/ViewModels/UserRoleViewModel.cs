using PODBookingSystem.Models;

namespace PODBookingSystem.ViewModels
{
    public class UserRoleViewModel
    {
        public Customer User { get; set; }
        public List<string> Roles { get; set; }
    }
}
