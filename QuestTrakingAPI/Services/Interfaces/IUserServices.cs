using QuestTrakingAPI.DataBase.DTO;
using QuestTrakingAPI.Response;

namespace QuestTrakingAPI.Services.Interfaces
{
    public interface IUserServices
    {
        public Task<GeneralResponse> AddendumUserAsync(RequestUser requestUser);
        public Task<UserResponse> GetAllUserAsync(RequestUser requestUser);
        public Task<UserResponse> GetUserByEmailAsync(string email);
        public Task<GeneralResponse> DeleteUserByEmailAsync(string email);
        public Task<UserResponse> UpdateUserByEmailAsync(string email, RequestUser requestUser);
    }
}
