using QuestTrakingAPI.DataBase.DTO;
using QuestTrakingAPI.Response;

namespace QuestTrakingAPI.Services.Interfaces
{
    public interface IQuestServices
    {
        public Task<GeneralResponse> AddQuestAsync(RequestQuest requestQuest);
        public Task<QuestResponse> GetQuestsAllAsync();
        public Task<QuestResponse> GetQuestByUserEmailAsync(string email);
        public Task<QuestResponse> GetQuestByIdAsync(int id);
        public Task<GeneralResponse> DeleteQuestByIdAsync(int id);
        public Task<GeneralResponse> UpdateQuestByIdAsync(int id, RequestQuest requestQuest);
    }
}
