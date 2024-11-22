using Microsoft.EntityFrameworkCore;
using PODBookingSystem.Models;
using PODBookingSystem.Models.DTOs;

namespace PODBookingSystem.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _context;

        public RoomService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomDTO>> GetAllRoomsAsync()
        {
            return await _context.Rooms.Select(r => new RoomDTO
            {
                RoomId = r.RoomId,
                Name = r.Name,
                Description = r.Description,
                Capacity = r.Capacity,
                Price = r.Price
            }).ToListAsync();
        }

        public async Task<RoomDTO> GetRoomByIdAsync(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null) return null;

            return new RoomDTO
            {
                RoomId = room.RoomId,
                Name = room.Name,
                Description = room.Description,
                Capacity = room.Capacity,
                Price = room.Price
            };
        }

        public async Task<IEnumerable<RoomDTO>> GetBookedRoomsByUserId(int userId)
        {
            var bookedRooms = await _context.Bookings
                .Where(b => b.UserId == userId)
                .Select(b => b.RoomId)
                .Distinct()
                .ToListAsync();

            var rooms = await _context.Rooms
                .Where(r => bookedRooms.Contains(r.RoomId))
                .Select(r => new RoomDTO
                {
                    RoomId = r.RoomId,
                    Name = r.Name,
                    Description = r.Description,
                    Capacity = r.Capacity,
                    Price = r.Price
                })
                .ToListAsync();

            return rooms;
        }
        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate)
        {
            var bookedRooms = await _context.Bookings
                                             .Where(b => b.StartTime < endDate && b.EndTime > startDate)
                                             .Select(b => b.RoomId)
                                             .ToListAsync();

            return await _context.Rooms.Where(r => !bookedRooms.Contains(r.RoomId)).ToListAsync();
        }
    }
}
