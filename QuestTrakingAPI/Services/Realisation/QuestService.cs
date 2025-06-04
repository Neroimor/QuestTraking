using Microsoft.EntityFrameworkCore;
using QuestTrakingAPI.DataBase.DTO;
using QuestTrakingAPI.DataBase.Services;
using QuestTrakingAPI.Response;
using QuestTrakingAPI.Services.Interfaces;

namespace QuestTrakingAPI.Services.Realisation
{
    public class QuestService : IQuestServices
    {


        private readonly ILogger<QuestService> _logger;
        private readonly AppDBContext _context;
        public QuestService(ILogger<QuestService> logger, AppDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<GeneralResponse> AddQuestAsync(RequestQuest requestQuest)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == requestQuest.EmailUser);
            if (user == null)
            {
                _logger.LogWarning("User with email {Email} not found.", requestQuest.EmailUser);
                return GeneralResponse.Fail("User not found.");
            }
            var quest = MapToQuest(requestQuest, user);
            _logger.LogInformation("Adding quest for user with email");
            if (string.IsNullOrEmpty(quest.Title) || string.IsNullOrEmpty(quest.Description))
            {
                return GeneralResponse.Fail("Title and Description are required.");
            }
            if (await _context.Quests.AnyAsync(q => q.Title == quest.Title && q.UserId == user.Id))
            {
                return GeneralResponse.Fail("Quest with this title already exists for this user.");
            }
            _context.Quests.Add(quest);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Quest for user with email added successfully");
            return GeneralResponse.Success("Quest added successfully.");
        }

        private Quest MapToQuest(RequestQuest requestQuest, User user)
        {
            return new Quest
            {
                CreatedAt = DateTime.UtcNow,
                Description = requestQuest.Description,
                IsCompleted = requestQuest.IsCompleted,
                Title = requestQuest.Title,
                UserId = user.Id,
                User = user
            };
        }

        public async Task<GeneralResponse> DeleteQuestByIdAsync(int id)
        {
            if (id <= 0)
            {
                return GeneralResponse.Fail("Invalid quest ID.");
            }
            var quest = await _context.Quests.FirstOrDefaultAsync(q => q.Id == id);
            if (quest == null)
            {
                return GeneralResponse.Fail("Quest not found.");
            }
            _context.Quests.Remove(quest);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Quest with ID deleted successfully");
            return GeneralResponse.Success("Quest deleted successfully.");
        }

        public async Task<QuestResponse<Quest>> GetQuestByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new QuestResponse<Quest>
                {
                    Status = 400,
                    Message = "Invalid quest ID.",
                    Data = null
                };
            }
            var quest = await _context.Quests
                .Include(q => q.User)
                .FirstOrDefaultAsync(q => q.Id == id);
            if (quest == null)
            {
                return new QuestResponse<Quest>
                {
                    Status = 404,
                    Message = "Quest not found.",
                    Data = null
                };
            }
            _logger.LogInformation("Quest with ID retrieved successfully");
            return new QuestResponse<Quest>
            {
                Status = 200,
                Message = "Quest retrieved successfully.",
                Data = quest
            };

        }

        public async Task<QuestResponse<List<Quest>>> GetQuestByUserEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new QuestResponse<List<Quest>>
                {
                    Status = 400,
                    Message = "Email is required.",
                    Data = null
                };
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) {
                return new QuestResponse<List<Quest>>
                {
                    Status = 404,
                    Message = "User not found.",
                    Data = null
                };
            }
            var quests = await _context.Quests
                .Where(q => q.UserId == user.Id)
                .ToListAsync();
            if (quests == null || !quests.Any())
            {
                return new QuestResponse<List<Quest>>
                {
                    Status = 404,
                    Message = "No quests found for this user.",
                    Data = null
                };
            }
            _logger.LogInformation("Quests for user with email retrieved successfully");
            return new QuestResponse<List<Quest>>
            {
                Status = 200,
                Message = "Quests retrieved successfully.",
                Data = quests
            };
        }

        public async Task<QuestResponse<List<Quest>>> GetQuestsAllAsync()
        {
            var quests = await _context.Quests
                .Include(q => q.User)
                .ToListAsync();
            if (quests == null || !quests.Any())
            {
                return new QuestResponse<List<Quest>>
                {
                    Status = 404,
                    Message = "No quests found.",
                    Data = null
                };
            }
            _logger.LogInformation("All quests retrieved successfully");
            return new QuestResponse<List<Quest>>
            {
                Status = 200,
                Message = "Quests retrieved successfully.",
                Data = quests
            };
        }

        public async Task<GeneralResponse> UpdateQuestByIdAsync(int id, RequestQuest requestQuest)
        {
            if (id <= 0)
            {
                return GeneralResponse.Fail("Invalid quest ID.");
            }
            var quest = await _context.Quests.FirstOrDefaultAsync(q => q.Id == id);
            if (quest == null)
            {
                return GeneralResponse.Fail("Quest not found.");
            }
            if (string.IsNullOrEmpty(requestQuest.Title) || string.IsNullOrEmpty(requestQuest.Description))
            {
                return GeneralResponse.Fail("Title and Description are required.");
            }
            quest.Title = requestQuest.Title;
            quest.Description = requestQuest.Description;
            quest.IsCompleted = requestQuest.IsCompleted;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Quest with ID updated successfully");
            return GeneralResponse.Success("Quest updated successfully.");
        }
    }
}
