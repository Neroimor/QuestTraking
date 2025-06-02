using QuestTrakingAPI.DataBase.DTO;
using QuestTrakingAPI.Response;

namespace QuestTrakingAPI.Services.Interfaces
{
    public interface IUserServices
    {
        public Task<GeneralResponse> AddendumUserAsync(RequestUser requestUser);
        public Task<UserResponse<List<User>>> GetAllUserAsync();
        public Task<UserResponse<User>> GetUserByEmailAsync(string email);
        public Task<GeneralResponse> DeleteUserByEmailAsync(string email);
        public Task<UserResponse<User>> UpdateUserByEmailAsync(string email, RequestUser requestUser);
    }
}
