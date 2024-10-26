using Microsoft.AspNetCore.Mvc;
using PODBookingSystem.ViewModels;
using System.Security.Claims;

namespace PODBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Lấy giá trị userId từ Claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Kiểm tra nếu userIdClaim không null
                if (userIdClaim != null)
                {
                    // Chuyển đổi giá trị userId từ chuỗi sang số nguyên
                    var userId = int.Parse(userIdClaim);

                    // Xử lý logic với userId (ví dụ: lấy thông tin từ database)
                    var userProfile = _context.Users.Find(userId);
                    ViewBag.ProfileImage = userProfile?.ProfileImage; // Giả sử bạn có trường ProfileImage

                    return View(userProfile);
                }
                else
                {
                    // Nếu không có userId, xử lý trường hợp này (ví dụ: trả về lỗi hoặc thông báo)
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                // Nếu người dùng chưa đăng nhập, điều hướng họ đến trang đăng nhập
                return RedirectToAction("Login", "Account");
            }
        }
    }

}