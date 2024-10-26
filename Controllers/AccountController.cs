using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PODBookingSystem.Services;
using System.Security.Claims;
using PODBookingSystem.ViewModels;
using PODBookingSystem.Models;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.EntityFrameworkCore;


public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IRoomService _roomService;

    public AccountController(IUserService userService, ApplicationDbContext context, IRoomService roomService, IWebHostEnvironment hostingEnvironment)
    {
        _userService = userService;
        _context = context;
        _roomService = roomService;
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    // POST: Account/Register
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Tạo một đối tượng User mới
            var user = new User
            {
                Name = model.Username,
                Email = model.Email,
                Password = model.Password, // Không hash mật khẩu
                Role = "Customer", // Gán vai trò mặc định là Customer
                ProfileImage = "~/img/default_image_path.jpg",
                Title = model.Title
            };

            // Lưu vào database
            _context.Users.Add(user);
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

            // Điều hướng đến trang đăng nhập sau khi đăng ký thành công
            return RedirectToAction("Login", "Account");
        }

        // Nếu có lỗi, trả về lại view với model
        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        // Tìm user theo email
        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        // Kiểm tra mật khẩu không hash
        if (user != null && user.Password == password)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Claim chứa UserId
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("ProfileImage", user.ProfileImage ?? "~/img/default_image_path.jpg")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // Điều hướng dựa trên vai trò
            if (user.Role == "Admin")
                return RedirectToAction("Dashboard", "Admin");
            else if (user.Role == "Manager")
                return RedirectToAction("Dashboard", "Manager");
            else
                return RedirectToAction("Index", "Home"); // Điều hướng đến trang chủ cho Customer
        }

        ViewBag.ErrorMessage = "Email hoặc mật khẩu không chính xác!";
        return View();
    }

   [HttpGet]
    public IActionResult Profile()
    {
        if (User.Identity.IsAuthenticated)
        {
            // Lấy ID người dùng từ Claims
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userId, out int parsedUserId))
            {
                // Lấy dữ liệu người dùng từ dịch vụ
                var user = _userService.GetUserById(parsedUserId);

                if (user == null)
                {
                    return RedirectToAction("Error");  // Chuyển hướng nếu không tìm thấy người dùng
                }
                // Chuyển đổi dữ liệu sang ProfileViewModel
                var model = new ProfileViewModel
                {
                    Username = user.Name,
                    Email = user.Email,
                    Image = user.ProfileImage,
                    Title = user.Title,
                    Role = user.Role,
                    //BookedRooms = _roomService.GetBookedRoomsByUserId(parsedUserId)
                };

                return View(model);  // Trả về view với model ProfileViewModel
            }
        }

        // Nếu không đăng nhập hoặc không lấy được ID
        return RedirectToAction("Login", "Account");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProfile(ProfileViewModel model)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
        {
            return RedirectToAction("Login", "Account");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return NotFound();
        }

        user.Name = model.Username;
        user.Title = model.Title;

        // Xử lý tải lên file ảnh
        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var fileName = Path.GetFileName(model.ImageFile.FileName);
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "img", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(stream);
            }

            user.ProfileImage = "/img/" + fileName;
        }

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return RedirectToAction("Profile");
    }


    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
