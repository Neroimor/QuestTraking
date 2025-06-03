using Microsoft.EntityFrameworkCore;
using QuestTrakingAPI.DataBase.DTO;
using QuestTrakingAPI.DataBase.Services;
using QuestTrakingAPI.Response;
using QuestTrakingAPI.Services.Interfaces;

namespace QuestTrakingAPI.Services.Realisation
{
    public class UserService : IUserServices
    {
        private readonly ILogger<UserService> _logger;
        private readonly AppDBContext _context;
        public UserService(ILogger<UserService> logger, AppDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<GeneralResponse> AddendumUserAsync(RequestUser requestUser)
        {
            var user = MapToUser(requestUser);
            _logger.LogInformation("Adding user with email");
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Name))
            {
                return GeneralResponse.Fail("Email and Name are required.");
            }
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return GeneralResponse.Fail("User with this email already exists.");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User with email added successfully");
            return GeneralResponse.Success("User added successfully.");
        }

        private User MapToUser(RequestUser requestUser)
        {
            return new User
            {
                Email = requestUser.Email,
                Name = requestUser.Name,
                CreatedAt = DateTime.UtcNow

            };
        }

        public async Task<GeneralResponse> DeleteUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return GeneralResponse.Fail("Email are required");
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return GeneralResponse.Fail("User not found.");
                
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User with email deleted successfully.");
            return GeneralResponse.Success("User deleted successfully.");
        }

        public async Task<UserResponse<List<User>>> GetAllUserAsync()
        {
            var users = await _context.Users.ToListAsync();
            if (users == null || users.Count == 0)
            {
                return UserResponse<List<User>>.Fail("No users found.");
            }
            _logger.LogInformation("Retrieved all users successfully.");
            return UserResponse<List<User>>.Success(users, "Users retrieved successfully.");
        }

        public async Task<UserResponse<User>> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return UserResponse<User>.Fail("Email is required.");
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return UserResponse<User>.Fail("User not found.");
            }
            _logger.LogInformation("User with email retrieved successfully.");
            return UserResponse<User>.Success(user, "User retrieved successfully.");

        }

        public async Task<UserResponse<User>> UpdateUserByEmailAsync(string email, RequestUser requestUser)
        {
            if (string.IsNullOrEmpty(email))
            {
                return UserResponse<User>.Fail("Email is required.");
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return UserResponse<User>.Fail("User not found.");
            }
            user.Name = requestUser.Name;
            user.Email = requestUser.Email;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User with email updated successfully.");
            return UserResponse<User>.Success(user, "User updated successfully.");
        }

    }
}
