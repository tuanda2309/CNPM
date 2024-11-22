using PODBookingSystem.Models;

namespace PODBookingSystem.Services
{
    public interface INotificationService
    {
        Task CreateNotificationAsync(int userId, int bookingId, string title, string message);
        Task<List<Notification>> GetUserNotificationsAsync(int userId);
    }
}
