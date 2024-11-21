using Microsoft.AspNetCore.Mvc;
using PODBookingSystem.Models;
using PODBookingSystem.Services;

namespace PODBookingSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult ViewAvailableRooms()
        {
            var availableRooms = _customerService.GetAvailableRooms();
            return View(availableRooms);
        }

        [HttpPost]
        public IActionResult MakeBooking(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _customerService.BookRoom(booking);
                return RedirectToAction("ViewBookingHistory");
            }
            return View(booking);
        }

        public IActionResult ViewProfile()
        {
            var userProfile = _customerService.GetUserProfile();
            return View(userProfile);
        }


        public IActionResult ViewBookingHistory()
        {
            var bookingHistory = _customerService.GetBookingHistory();
            return View(bookingHistory);
        }
    }
}
