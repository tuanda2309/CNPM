using PODBookingSystem.Models;

namespace PODBookingSystem.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void DeleteUser(int id);
        User ValidateUser(string email, string password);
        void CreateUser(User user);
        void UpdateUser(User user);
        Task<string> GetUserEmailByIdAsync(int userId);
    }
}
