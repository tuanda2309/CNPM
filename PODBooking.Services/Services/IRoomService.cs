using PODBookingSystem.Models;
using PODBookingSystem.Models.DTOs;

namespace PODBookingSystem.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDTO>> GetAllRoomsAsync();
        Task<RoomDTO> GetRoomByIdAsync(int roomId);
        Task<IEnumerable<RoomDTO>> GetBookedRoomsByUserId(int userId); 
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate);
    }
}
