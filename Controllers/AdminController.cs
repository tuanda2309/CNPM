using Microsoft.AspNetCore.Mvc;
using PODBookingSystem.Models;
using PODBookingSystem.ViewModels;
using System;
using System.Security.Claims;

namespace PODBookingSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Create(string username, string email, string password, string role)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    Name = username,
                    Email = email,
                    Password = password,
                    Role = role,
                    ProfileImage = "~/img/default_image_path.jpg",
                    Title = "Hi, I'm Ca Voi"
                };
                _context.Users.Add(newUser);
                _context.SaveChanges(); 
                return RedirectToAction("Dashboard", "Admin");
            }
            return View();
        }

        [HttpPost]
            public IActionResult UploadProfileImage(IFormFile file)
            {
                if (file != null && file.Length > 0)
                {
                   
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", file.FileName);

                  
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var user = _context.Users.FirstOrDefault(u => u.Id == int.Parse(userId));

                    if (user != null)
                    {
                        user.ProfileImage = "/img/" + file.FileName; 
                        _context.SaveChanges();
                    }

                    
                    return RedirectToAction("ProfileAdmin");
                }

               
                ModelState.AddModelError("", "Có lỗi khi tải ảnh lên.");
                return RedirectToAction("ProfileAdmin");
            }

        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult ProfileAdmin()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Bạn chưa đăng nhập.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == int.Parse(userId));

            if (user == null)
            {
                return NotFound("Người dùng không tồn tại!");
            }

            var model = new AdminProfileViewModel
            {
                Username = user.Name,
                Email = user.Email,
                Role = user.Role
            };

            return View(model);
        }


        public IActionResult ProfileUsers()
        {
            return View();
        }
        public IActionResult Rooms()
        {
            return View("Rooms");
        }
        public IActionResult Revenue()
        {
            
            var revenueData = _context.Bookings
                .Where(b => b.Status == "Confirmed")
                .GroupBy(b => new { b.BookingDate.Year, b.BookingDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalRevenue = g.Sum(b => b.TotalPrice) / 1000000
                })
                .OrderBy(g => g.Year).ThenBy(g => g.Month)
                .ToList();

          
            var roomRevenueData = _context.Bookings
                .Where(b => b.Status == "Confirmed")
                .GroupBy(b => b.RoomId)
                .Select(g => new
                {
                    RoomId = g.Key,
                    TotalRevenue = g.Sum(b => b.TotalPrice) / 1000000
                })
                .ToList();

           
            if (revenueData == null || roomRevenueData == null)
            {
                return View("Error"); 
            }

            ViewBag.RevenueData = revenueData;
            ViewBag.RoomRevenueData = roomRevenueData;

            return View();
        }

    }
}
