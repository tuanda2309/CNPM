using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PODBookingSystem.Models.DTOs;
using PODBookingSystem.Services;
using System.Threading.Tasks;

namespace PODBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IBookingService _servicePackageService;

        public BookingController(IBookingService bookingService, IBookingService servicePackageService)
        {
            _bookingService = bookingService;
            _servicePackageService = servicePackageService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateBooking()
        {
            var servicePackages = await _servicePackageService.GetAllServicePackages();

            // Chuyển đổi danh sách ServicePackageDTO thành SelectListItem
            var servicePackageSelectList = servicePackages.Select(sp => new SelectListItem
            {
                Value = sp.ServicePackageId.ToString(), // Giả sử Id là thuộc tính của ServicePackageDTO
                Text = sp.Name // Giả sử Name là thuộc tính bạn muốn hiển thị
            }).ToList();

            var bookingDto = new BookingDTO
            {
                ServicePackageOptions = servicePackageSelectList // Gán danh sách vào thuộc tính trong BookingDTO
            };

            return View(bookingDto); // Trả về view với danh sách gói dịch vụ
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingDTO bookingDto)
        {
            if (ModelState.IsValid)
            {
                // Gọi dịch vụ để tạo đặt phòng
                var result = await _bookingService.CreateBooking(bookingDto);

                // Kiểm tra kết quả trả về từ dịch vụ
                if (result.Status == "Pending")
                {
                    // Chuyển đến trang thành công
                    return RedirectToAction("BookingSuccess");
                }
                else if (result.Status == "Failed")
                {
                    ModelState.AddModelError("", "Không thể đặt phòng. Vui lòng kiểm tra lại thông tin và thử lại.");
                }
            }

            // Nếu có lỗi, lấy lại danh sách gói dịch v
            return View(bookingDto); // Trả lại trang tạo đặt phòng với thông tin đã nhập
        }


        public async Task<IActionResult> ListBookings()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            // Chuyển đổi sang int
            if (int.TryParse(userIdClaim, out int userId))
            {
                var bookings = await _bookingService.GetUserBookings(userId);
                return View(bookings); // Trả về trang danh sách đặt phòng
            }

            // Nếu không tìm thấy UserId, có thể quay lại hoặc hiển thị thông báo lỗi
            return BadRequest("User ID không hợp lệ.");
        }

        public IActionResult BookingSuccess()
        {
            return View(); // Trả về trang thông báo đặt phòng thành công
        }

        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            var result = await _bookingService.CancelBooking(bookingId);
            if (result)
            {
                return RedirectToAction("ListBookings"); // Chuyển đến trang danh sách đặt phòng
            }
            return View("Error"); // Nếu không thành công, hiển thị trang lỗi
        }
    }
}
