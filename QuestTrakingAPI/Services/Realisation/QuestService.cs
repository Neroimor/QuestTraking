using QuestTrakingAPI.DataBase.DTO;
using QuestTrakingAPI.Response;
using QuestTrakingAPI.Services.Interfaces;

namespace QuestTrakingAPI.Services.Realisation
{
    public class QuestService : IQuestServices
    {
        public Task<GeneralResponse> AddQuestAsync(RequestQuest requestQuest)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteQuestByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<QuestResponse> GetQuestByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<QuestResponse> GetQuestByUserEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<QuestResponse> GetQuestsAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> UpdateQuestByIdAsync(int id, RequestQuest requestQuest)
        {
            throw new NotImplementedException();
        }
    }
}
