using Microsoft.EntityFrameworkCore;
using PODBookingSystem.Models;
using PODBookingSystem.Models.DTOs;

namespace PODBookingSystem.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRoomService _roomService;
        private readonly ApplicationDbContext _context;

        public BookingService(IRoomService roomService, ApplicationDbContext context)
        {
            _roomService = roomService;
            _context = context;
        }

        public async Task<double> CalculateBookingCost(int roomId, DateTime startTime, DateTime endTime)
        {
            var room = await _roomService.GetRoomByIdAsync(roomId);
            if (room == null)
            {
                throw new Exception("Phòng không tồn tại.");
            }

            var duration = (endTime - startTime).TotalHours;
            var cost = duration * room.Price; 
            return cost;
        }

        public async Task<int> CreateBooking(BookingDTO bookingDto)
        {
            var bookingCost = await CalculateBookingCost(bookingDto.RoomId, bookingDto.StartTime, bookingDto.EndTime);
            bookingDto.TotalPrice = bookingCost; 

            var booking = new Booking
            {
                RoomId = bookingDto.RoomId,
                UserId = bookingDto.CustomerId,
                StartTime = bookingDto.StartTime,
                EndTime = bookingDto.EndTime,
                TotalPrice = bookingDto.TotalPrice,
                Status = "spending",
                PaymentStatus = "Pending"
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return booking.BookingId; 
        }

        public async Task<bool> CheckRoomAvailability(int roomId, DateTime startTime, DateTime endTime)
        {
            return !await _context.Bookings.AnyAsync(b =>
                b.RoomId == roomId &&
                ((startTime >= b.StartTime && startTime < b.EndTime) ||
                (endTime > b.StartTime && endTime <= b.EndTime) ||
                (startTime <= b.StartTime && endTime >= b.EndTime)));
        }

        public async Task<List<BookingDTO>> GetAllBookingsAsync()
        {
            return await _context.Bookings
                .Select(b => new BookingDTO
                {
                    BookingId = b.BookingId,
                    RoomId = b.RoomId,
                    CustomerId = (int)b.UserId,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime,
                    TotalPrice = b.TotalPrice,
                    Status = b.Status
                }).ToListAsync();
        }

        public async Task UpdateBookingStatusAsync(int bookingId, string newStatus)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                throw new Exception("Booking không tồn tại.");
            }

            booking.Status = newStatus;
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            return await _context.Bookings.FindAsync(bookingId);
        }

        public async Task<Room> GetRoomByIdAsync(int roomId)
        {
            return await _context.Rooms.FindAsync(roomId);
        }

        public async Task<List<BookingDTO>> GetConfirmedBookingsAsync(DateTime startDate, DateTime endDate, string userId)
        {
            return await _context.Bookings
                .Where(b => b.Status == "Confirmed"
                            && b.StartTime >= startDate
                            && b.EndTime <= endDate
                            && (string.IsNullOrEmpty(userId) || b.UserId == int.Parse(userId)))
                .Join(
                    _context.Users,
                    booking => booking.UserId,
                    user => user.Id,
                    (booking, user) => new { booking, user }
                )
                .Join(
                    _context.Rooms, 
                    data => data.booking.RoomId,
                    room => room.RoomId,
                    (data, room) => new { data.booking, data.user, room }
                )
                .Select(data => new BookingDTO
                {
                    BookingId = data.booking.BookingId,
                    CustomerId = (int)data.booking.UserId,
                    CustomerName = data.user.Name,
                    RoomName = data.room.Name,
                    StartTime = data.booking.StartTime,
                    EndTime = data.booking.EndTime,
                    Status = data.booking.Status
                })
                .ToListAsync();
        }


    }
}
