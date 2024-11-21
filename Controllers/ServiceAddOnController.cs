using Microsoft.AspNetCore.Mvc;

namespace PODBookingSystem.Controllers
{
    public class ServiceAddOnController : Controller
    {
        public IActionResult Service()
        {
            return View("~/Views/Service/Service.cshtml");
        }
    }
}