using PODBookingSystem.Models;

namespace PODBookingSystem.Services
{
    public interface ICustomerService
    {
        List<Room> GetAvailableRooms();
        void BookRoom(Booking booking);
        User GetUserProfile();
        List<Booking> GetBookingHistory();
    }
}
