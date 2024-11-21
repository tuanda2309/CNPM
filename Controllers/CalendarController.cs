using Microsoft.AspNetCore.Mvc;
using PODBookingSystem.Services;
using PODBookingSystem.ViewModels;
using System.Security.Claims;

public class CalendarController : Controller
{
    private readonly BookingService _bookingService;

    public CalendarController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public ClaimsPrincipal GetUser()
    {
        return User;
    }

    public async Task<IActionResult> Calendar(ClaimsPrincipal user)
    {
        DateTime currentDate = DateTime.Now;
        DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + 1);
        DateTime endOfWeek = startOfWeek.AddDays(6);

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        var bookings = await _bookingService.GetConfirmedBookingsAsync(startOfWeek, endOfWeek, userId);

        var viewModel = new CalendarViewModel
        {
            CurrentWeekStart = startOfWeek,
            CurrentWeekEnd = endOfWeek,
            Bookings = bookings
        };

        return View(viewModel);
    }

}