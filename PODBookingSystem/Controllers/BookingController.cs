using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PODBooking.Repositories.Models;
using PODBooking.Services.Services.Momo;
using PODBookingSystem.Models.DTOs;
using PODBookingSystem.Services;
using System.Security.Claims;
using PODBookingSystem.Models;
namespace PODBookingSystem.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;
        private readonly IMomoService _momoService;
        private readonly ApplicationDbContext _context;
        private readonly IRoomService _roomService;

        public BookingController(IBookingService bookingService, INotificationService notificationService, IUserService userService, IPaymentService paymentService, IMomoService momoService, ApplicationDbContext context,IRoomService roomService)
        {
            _bookingService = bookingService;
            _notificationService = notificationService;
            _userService = userService;
            _paymentService = paymentService;
            _momoService = momoService;
            _context = context;
            _roomService = roomService;
        }

        [HttpGet]
        public IActionResult CreateBooking(int roomId)
        {
            ViewBag.RoomId = roomId;
            var bookingDto = new BookingDTO
            {
                RoomId = roomId
            };
            return View(bookingDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingDTO bookingDto, string PaymentMethod)
        {
            if (ModelState.IsValid)
            {
                bookingDto.CustomerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var isAvailable = await _bookingService.CheckRoomAvailability(bookingDto.RoomId, bookingDto.StartTime, bookingDto.EndTime);
                if (isAvailable)
                {
                    // MOMO
                    if (PaymentMethod == "momo")
                    {
                        int bookingId = await _bookingService.CreateBooking(bookingDto); 
                        var orderInfo = new OrderInfo
                        {
                            OrderInformation = "Thanh toán đặt phòng",
                            Amount = bookingDto.TotalPrice,
                            OrderId = bookingId.ToString(), 
                            FullName = "PobBooking"
                        };
                        var paymentResponse = await _momoService.CreatePaymentMomo(orderInfo);
                        return Redirect(paymentResponse.PayUrl);
                    }

                    // NGÂN HÀNG
                    else if (PaymentMethod == "bank")
                    {
                        var booking = new Bank
                        {
                            UserId = bookingDto.CustomerId,
                            RoomId = bookingDto.RoomId,
                            StartTime = bookingDto.StartTime,
                            EndTime = bookingDto.EndTime,
                            BookingDate = DateTime.Now, 
                            TotalPrice = bookingDto.TotalPrice,
                            Status = "Đang chờ", 
                            PaymentStatus = "Chưa thanh toán" 
                        };
                        _context.Banks.Add(booking);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("BookingSuccess");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Phòng đã được đặt trong khoảng thời gian này.");
                }
            }
            return View(bookingDto);
        }


        [HttpGet]
        public IActionResult BookingSuccess()
        {
            return View(); 
        }

        [HttpGet]
        public async Task<IActionResult> ManageBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return View(bookings); 
        }
        
        [HttpPost]
            public async Task<IActionResult> UpdateBookingStatus(int bookingId, string newStatus)
            {
                var booking = await _context.Bookings.FindAsync(bookingId);
                if (booking == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy đặt phòng.";
                    return RedirectToAction("ManageBookings");
                }

                booking.Status = newStatus;
                _context.Bookings.Update(booking);
                await _context.SaveChangesAsync();

                // Tạo thông báo dựa trên trạng thái mới
                if (newStatus == "Confirmed")
                {
                    var notification = new Notification
                    {
                        UserId = (int)booking.UserId, // ID của khách hàng
                        Title = "Thông báo đặt phòng",
                        Message = "Phòng bạn đặt thành công.",
                        CreatedAt = DateTime.Now
                    };
                    _context.Notifications.Add(notification);
                }
                else if (newStatus == "Cancelled")
                {
                    var notification = new Notification
                    {
                        UserId = (int)booking.UserId, // ID của khách hàng
                        Title = "Thông báo đặt phòng",
                        Message = "Phòng bạn đã bị hủy.",
                        CreatedAt = DateTime.Now
                    };
                    _context.Notifications.Add(notification);
                }

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Cập nhật trạng thái thành công.";
                return RedirectToAction("ManageBookings");
            }



        [HttpGet]
        public async Task<IActionResult> UserNotifications()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var notifications = await _notificationService.GetUserNotificationsAsync(userId);
            return View(notifications);
        }
        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
