using QuestTrakingAPI.DataBase.DTO;
using QuestTrakingAPI.Response;
using QuestTrakingAPI.Services.Interfaces;

namespace QuestTrakingAPI.Services.Realisation
{
    public class UserService : IUserServices
    {
        public Task<GeneralResponse> AddendumUserAsync(RequestUser requestUser)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> GetAllUserAsync(RequestUser requestUser)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> UpdateUserByEmailAsync(string email, RequestUser requestUser)
        {
            throw new NotImplementedException();
        }
    }
}
