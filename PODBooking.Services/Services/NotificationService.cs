using Microsoft.EntityFrameworkCore;
using PODBookingSystem.Models;

namespace PODBookingSystem.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateNotificationAsync(int userId, int bookingId, string title, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                BookingId = bookingId,
                Title = title,
                Message = message,
                CreatedAt = DateTime.Now,
                IsRead = false
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Notification>> GetUserNotificationsAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }


    }
}
