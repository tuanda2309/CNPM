using Microsoft.EntityFrameworkCore;
using PODBookingSystem.Models;

namespace PODBookingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context; 

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả người dùng từ cơ sở dữ liệu
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        // Xóa người dùng theo ID từ cơ sở dữ liệu
        public void DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges(); 
            }
        }

        // Xác thực người dùng bằng email và mật khẩu từ cơ sở dữ liệu
        public async Task<User> ValidateUser(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null && user.Password == password) 
            {
                return user; 
            }
            return null; 
        }



        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void UpdateUser(User user)
        {
            var existingUser = _context.Users.Find(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password; 
                _context.SaveChanges();
            }
        }

        User IUserService.ValidateUser(string email, string password)
        {
            throw new NotImplementedException();
        }
        public async Task<string> GetUserEmailByIdAsync(int userId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.Email)
                .FirstOrDefaultAsync();

            return user; 
        }
    }
}
