using Microsoft.AspNetCore.Mvc;
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
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim != null && int.TryParse(userIdClaim, out int userId))
                {
                    var userProfile = _context.Users.Find(userId);
                    if (userProfile != null)
                    {
                        ViewBag.ProfileImage = userProfile.ProfileImage ?? "default_image_path"; 

                        return View(userProfile);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home"); 
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
