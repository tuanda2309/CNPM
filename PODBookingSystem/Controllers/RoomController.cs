using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PODBookingSystem.Models;
using PODBookingSystem.Services;
using PODBookingSystem.ViewModels;

namespace PODBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // API để lấy URL PhongTrongNgay
        [HttpGet("PhongTrongNgayUrl")]
        public IActionResult GetPhongTrongNgayUrl()
        {
            var url = Url.Action("PhongTrongNgay", "Room");
            return Ok(new { url });
        }

        // API để lấy URL PhongHop
        [HttpGet("PhongHopUrl")]
        public IActionResult GetPhongHopUrl()
        {
            var url = Url.Action("PhongHop", "Room");
            return Ok(new { url });
        }

        // API để lấy URL VIP
        [HttpGet("PhongVipUrl")]
        public IActionResult GetPhongVipUrl()
        {
            var url = Url.Action("Vip", "Room");
            return Ok(new { url });
        }

    }
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
            return View();  
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
                model.ImageUrl = "/img/" + uniqueFileName;  
                Console.WriteLine("ImageUrl: " + model.ImageUrl);  
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
                    Image = model.ImageUrl, 
                    Location = model.Address,
                    UserId = model.Id,
                    Description = model.Description,
                    Price = model.Price, 
                    IsAvailable = true, 
                    CreatedBy = User.Identity.Name 
                };

                _context.Rooms.Add(room);  // Thêm phòng vào database
                await _context.SaveChangesAsync();

                return RedirectToAction("SeeRoom", "Room");
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
                Console.WriteLine("ImageUrl: " + model.ImageUrl);
            }

            return View("CreateRoom", model);  
        }

        public async Task<IActionResult> SeeRoom()
        {
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
            ViewBag.ProfileImage = profileImage;
            return View(rooms); 
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
                    OwnerName = r.OwnerName,
                    Price = r.Price
                })
                .FirstOrDefault();

            if (room == null)
            {
                return NotFound();
            }

            return View(room);  
        }
        public IActionResult PhongTrongNgay()
        {
            var room = new RoomViewModel { Name = "Văn phòng trong ngày" };  
            return View("P", room);  
        }

        // Phòng họp
        public IActionResult PhongHop()
        {
            var room = new RoomViewModel { Name = "Phòng Họp" };  
            return View("P1", room);  
        }

        // Phòng VIP
        public IActionResult Vip()
        {
            var room = new RoomViewModel { Name = "Phòng VIP" };  
            return View("Vip", room);  
        }

        // Phòng 1
        public IActionResult Phong1()
        {
            var room = new RoomViewModel { Name = "Phòng 1" };  
            return View("Phong1", room);  
        }

        // Phòng 2
        public IActionResult Phong2()
        {
            var room = new RoomViewModel { Name = "Phòng 2" };  
            return View("Phong2", room);
        }
        // Phòng 3
        public IActionResult Phong3()
        {
            var room = new RoomViewModel { Name = "Phòng 3" };  
            return View("Phong3", room);
        }
        // Phòng 4
        public IActionResult Phong4()
        {
            var room = new RoomViewModel { Name = "Phòng 4" };  
            return View("Phong4", room);
        }

        // Trang lỗi 404 tùy chỉnh
        [Route("Room/404")]
        public IActionResult NotFoundPage()
        {
            return View("404");
        }

    }
}
