using PODBookingSystem.Models;
using PODBookingSystem.Models.DTOs;

namespace PODBookingSystem.Services
{
    public interface IBookingService
    {
        Task<bool> CheckRoomAvailability(int roomId, DateTime startTime, DateTime endTime);
        Task<int> CreateBooking(BookingDTO bookingDto);
        Task<double> CalculateBookingCost(int roomId, DateTime startTime, DateTime endTime);
        Task<List<BookingDTO>> GetAllBookingsAsync();
        Task UpdateBookingStatusAsync(int bookingId, string newStatus);
        Task<Booking> GetBookingByIdAsync(int bookingId);
        Task<Room> GetRoomByIdAsync(int roomId);
        Task<List<BookingDTO>> GetConfirmedBookingsAsync(DateTime startDate, DateTime endDate,string userId);
    }
}
