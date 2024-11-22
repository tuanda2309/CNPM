using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PODBookingSystem.Models;
using PODBookingSystem.Services;
using PODBookingSystem.ViewModels;
using System.Security.Claims;


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

    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                Name = model.Username,
                Email = model.Email,
                Password = model.Password, 
                Role = "Customer", 
                ProfileImage = "~/img/default_image_path.jpg",
                Title = model.Title
            };

            // Lưu vào database
            _context.Users.Add(user);
            await _context.SaveChangesAsync(); 

            return RedirectToAction("Login", "Account");
        }

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
        if (string.IsNullOrEmpty(email))
        {
            ModelState.AddModelError("Email", "Vui lòng nhập email");
        }
        if (string.IsNullOrEmpty(password))
        {
            ModelState.AddModelError("Password", "Vui lòng nhập mật khẩu");
        }
        if (!ModelState.IsValid)
        {
            return View();
        }
        // Tìm user theo email
        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        if (user == null)
        {
            TempData["EmailErrorMessage"] = "Email không tồn tại";
            return View();
        }

        if (user.Password != password)
        {
            TempData["PasswordErrorMessage"] = "Mật khẩu không chính xác";
            return View();
        }
        if (user != null && user.Password == password)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
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

            if (user.Role == "Admin")
                return RedirectToAction("Dashboard", "Admin");
            else
                return RedirectToAction("Index", "Home"); 
        }

        TempData["ErrorMessage"] = "Thông tin đăng nhập không hợp lệ";
        return View();
    }

    [HttpGet]
    public IActionResult Profile()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userId, out int parsedUserId))
            {
                var user = _userService.GetUserById(parsedUserId);

                if (user == null)
                {
                    return RedirectToAction("Error");  
                }
                var model = new ProfileViewModel
                {
                    Username = user.Name,
                    Email = user.Email,
                    Image = user.ProfileImage,
                    Title = user.Title,
                    Role = user.Role,
                };

                return View(model);
            }
        }
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
    [HttpGet]
    public IActionResult LoginWithGoogle(string returnUrl = null)
    {
        var redirectUrl = Url.Action("GoogleResponse", "Account", new { returnUrl });
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }
    [HttpGet]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (!result.Succeeded)
        {
            return RedirectToAction("Login");
        }

        // Lấy thông tin người dùng từ Google
        var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
        var name = result.Principal.FindFirst(ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(email))
        {
            TempData["ErrorMessage"] = "Không thể lấy thông tin email từ Google.";
            return RedirectToAction("Login");
        }

        if (string.IsNullOrEmpty(name))
        {
            TempData["ErrorMessage"] = "Tên không được để trống, vui lòng nhập tên của bạn.";
            return RedirectToAction("CompleteProfile", new { email = email });
        }

        var existingUser = await _context.Users
            .Where(u => u.Email.Trim().ToLower() == email.Trim().ToLower())
            .FirstOrDefaultAsync();

        User user;
        if (existingUser == null)
        {
            user = new User
            {
                Email = email,
                Name = name,
                Role = "Customer",
                ProfileImage = "~/img/default_image_path.jpg",
                Password = "123456",  
                Title = "Chào mừng bạn"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        else
        {
            user = existingUser;
        }

        await _context.SaveChangesAsync();

        // Đăng nhập vào hệ thống
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role)
    };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return RedirectToAction("Index", "Home");
    }

}
