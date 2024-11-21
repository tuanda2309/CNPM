using Microsoft.AspNetCore.Mvc;
using PODBookingSystem.Services;
using System.Security.Claims;

namespace PODBookingSystem.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly ApplicationDbContext _context;

        public NotificationController(INotificationService notificationService, ApplicationDbContext context)
        {
            _notificationService = notificationService;
            _context = context;
        }

        public async Task<IActionResult> GetUserNotifications()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var notifications = await _notificationService.GetUserNotificationsAsync(userId);
            return PartialView("_NotificationList", notifications);
        }
        
        [HttpGet]
        public IActionResult Details(int id)
        {
            var notification = _context.Notifications.FirstOrDefault(n => n.NotificationId == id);

            if (notification == null)
            {
                return NotFound(); 
            }
            return View("Details", notification);
        }
    }

}
