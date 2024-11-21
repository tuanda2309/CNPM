using Microsoft.AspNetCore.Mvc;
using PODBooking.Repositories.Models;
using PODBooking.Repositories.Models.Momo;
using PODBooking.Services.Services.Momo;

namespace PODBookingSystem.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IMomoService _momoService;

        public PaymentController(IMomoService momoService)
        {
            _momoService = momoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentMomo(OrderInfo model)
        {
            var response = await _momoService.CreatePaymentMomo(model);
            return Redirect(response.PayUrl);
        }
        [HttpGet]
        public IActionResult PaymentCallBack()
        {
            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            return View(response);
        }

    }
}
