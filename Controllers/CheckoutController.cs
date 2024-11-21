using Microsoft.AspNetCore.Mvc;
using PODBooking.Repositories.Models;
using PODBooking.Services.Services.Momo;

namespace PODBookingSystem.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMomoService _momoService;

        public CheckoutController(ApplicationDbContext context, IMomoService momoService)
        {
            _context = context;
            _momoService = momoService;
        }

        public async Task<IActionResult> PaymentCallBack(MomoInfoModel model)
        {
            var requestQuery = HttpContext.Request.Query; 
            var response = _momoService.PaymentExecuteAsync(requestQuery); 
            return View(response);
        }
    }
}
