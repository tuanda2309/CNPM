using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PODBookingSystem.Models;
using PODBookingSystem.Services;
using PODBookingSystem.ViewModels;
using System.IO;
using System.Threading.Tasks;

namespace PODBookingSystem.Controllers
{
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRoomService _roomService;

        public RoomController(ApplicationDbContext context, IRoomService roomService)
        {
            _context = context;
            _roomService = roomService;
        }

        [HttpGet]
        public IActionResult CreateRoom()
        {
            return View();  // Trả về trang CreateRoom.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRoom(RoomViewModel model)
        {
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }
                model.ImageUrl = "/img/" + uniqueFileName;  // Gán đúng URL
                Console.WriteLine("ImageUrl: " + model.ImageUrl);  // Kiểm tra URL
            }
            else
            {
                ModelState.AddModelError("ImageUrl", "Hãy chọn một hình ảnh.");
            }
            if (ModelState.IsValid)
            {
                // Lưu thông tin phòng vào database
                var room = new Room
                {
                    Name = model.Name,
                    OwnerName = model.OwnerName,
                    DatePosted = model.DatePosted,
                    Image = model.ImageUrl, // Đảm bảo ImageUrl được gán
                    Location = model.Address,
                    Description = model.Description,
                    Price = model.Price, // Cập nhật giá phòng
                    IsAvailable = true, // Mặc định phòng có sẵn
                    CreatedBy = User.Identity.Name // Ghi nhận người tạo
                };

                _context.Rooms.Add(room);  // Thêm phòng vào database
                await _context.SaveChangesAsync();

                // Chuyển hướng đến trang SeeRoom sau khi submit thành công
                return RedirectToAction("SeeRoom", "Room");
            }

            // In ra các lỗi nếu có
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
                Console.WriteLine("ImageUrl: " + model.ImageUrl);
            }

            return View("CreateRoom", model);  // Nếu không hợp lệ, quay lại form
        }

        public async Task<IActionResult> SeeRoom()
        {
            // Lấy danh sách phòng từ database
            var rooms = await _context.Rooms
                .Select(r => new RoomViewModel
                {
                    Id = r.RoomId,
                    Name = r.Name,
                    Address = r.Location,
                    Description = r.Description,
                    ImageUrl = r.Image,
                    OwnerName = r.OwnerName
                }).ToListAsync();
            var profileImage = User.FindFirst("ProfileImage")?.Value ?? "~/img/default_image_path.jpg";

            // Truyền profileImage vào view
            ViewBag.ProfileImage = profileImage;
            return View(rooms);  // Trả về trang SeeRoom.cshtml với danh sách phòng
        }

        public IActionResult Details(int id)
        {
            // Lấy chi tiết của một phòng từ database
            var room = _context.Rooms
                .Where(r => r.RoomId == id)
                .Select(r => new RoomViewModel
                {
                    Id = r.RoomId,
                    Name = r.Name,
                    Address = r.Location,
                    Description = r.Description,
                    ImageUrl = r.Image,
                    OwnerName = r.OwnerName
                })
                .FirstOrDefault();

            if (room == null)
            {
                return NotFound();
            }

            return View(room);  // Trả về trang Details.cshtml với thông tin chi tiết của phòng
        }
        /*public IActionResult Search(string name, double? price, TimeSpan? time)
        {
            var rooms = _context.Rooms.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                rooms = rooms.Where(r => r.Name.Contains(name));
            }

            if (price.HasValue)
            {
                rooms = rooms.Where(r => r.Price <= price.Value);
            }

            if (time.HasValue)
            {
                rooms = rooms.Where(r => r.AvailableFrom <= time && r.AvailableTo >= time);
            }

            return View("SeeRoom", rooms.ToList());
        }*/

    }
}
