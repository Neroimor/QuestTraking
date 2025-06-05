using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using QuestTrakingAPI.DataBase.DTO;
using QuestTrakingAPI.DataBase.Services;
using QuestTrakingAPI.Services.Interfaces;
using QuestTrakingAPI.Services.Realisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestQuestTrakingApi
{
    public class QuestServiceTest : IDisposable
    {
        private readonly QuestService _questService;
        private readonly UserService _userService;
        private readonly Mock<ILogger<QuestService>> _loggerMock;
        private readonly Mock<ILogger<UserService>> _loggerMockU;
        private readonly AppDBContext _contextMock;
        public QuestServiceTest()
        {
            _loggerMock = new Mock<ILogger<QuestService>>();
            _loggerMockU = new Mock<ILogger<UserService>>();

            var options = new DbContextOptionsBuilder<AppDBContext>()
                 .UseInMemoryDatabase(databaseName: "TestDatabaseQuest")
                 .Options;
            _contextMock = new AppDBContext(options);

            _questService = new QuestService(_loggerMock.Object, _contextMock);
            _userService = new UserService(_loggerMockU.Object, _contextMock);

            _contextMock.Database.EnsureDeleted();
            _contextMock.Database.EnsureCreated();

        }


        [Fact]
        public async Task TestAddQuest()
        {
            var user = await AddUser();
            var quest = CreateQuest(user.Email);
            var response = await _questService.AddQuestAsync(quest);
            Assert.NotNull(response);
            Assert.Equal("Quest added successfully.", response.Message);
            Assert.Equal(200, response.Status);

        }


        [Fact]
        public async Task TestGetAllQuest()
        {
            var user = await AddUser();
            var quest = CreateQuest(user.Email);
            await _questService.AddQuestAsync(quest);

            var quests = await _questService.GetQuestsAllAsync();

            Assert.NotNull(quests);
            Assert.Equal(200, quests.Status);
            Assert.NotNull(quests.Data);
            Assert.Single(quests.Data);

        }

        [Fact]
        public async Task TestGetQuestByUserEmailAsync()
        {
            var user = await AddUser();
            var quest = CreateQuest(user.Email);
            await _questService.AddQuestAsync(quest);
            var quests = await _questService.GetQuestByUserEmailAsync(user.Email);
            Assert.NotNull(quests);
            Assert.Equal(200, quests.Status);
            Assert.NotNull(quests.Data);
            Assert.Single(quests.Data);
            Assert.Equal(quest.Title, quests.Data.First().Title);
            Assert.Equal(quest.Description, quests.Data.First().Description);
            Assert.Equal(quest.IsCompleted, quests.Data.First().IsCompleted);
            Assert.Equal(quest.EmailUser, quests.Data.First().User?.Email);
        }

        [Fact]
        public async Task TestGetQuestById()
        {
            var user = await AddUser();
            var quest = CreateQuest(user.Email);
            await _questService.AddQuestAsync(quest);
            var questsData = await _questService.GetQuestByUserEmailAsync(user.Email);


            var quests = await _questService.GetQuestByIdAsync(questsData.Data.First().Id);
            Assert.NotNull(quests);
            Assert.Equal(200, quests.Status);
            Assert.NotNull(quests.Data);
            Assert.Equal("Quest retrieved successfully.", quests.Message);

        }

        [Fact]
        public async Task TestDeleteQuestById()
        {
            var user = await AddUser();
            var quest = CreateQuest(user.Email);
            await _questService.AddQuestAsync(quest);
            var questsData = await _questService.GetQuestByUserEmailAsync(user.Email);


            var response = await _questService.DeleteQuestByIdAsync(questsData.Data.First().Id);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task TestUpdateQuest()
        {
            var user = await AddUser();
            var quest = CreateQuest(user.Email);
            var updateQuest = UpdateQuest(user.Email);

            await _questService.AddQuestAsync(quest);
            var questsData = await _questService.GetQuestByUserEmailAsync(user.Email);
            Assert.NotNull(questsData.Data);
            var response = await _questService.UpdateQuestByIdAsync(questsData.Data.First().Id, updateQuest);
            Assert.NotNull(response);
            Assert.Equal("Quest updated successfully.", response.Message);
            Assert.Equal(200, response.Status);
            Assert.NotNull(response);

        }


        private RequestQuest CreateQuest(string emailUser)
        {
            return new RequestQuest()
            {
                Title = "FOR THE EMPERIAR",
                Description = "Kill 1000 orcs",
                IsCompleted = false,
                EmailUser = emailUser
            };
        }
        private RequestQuest UpdateQuest(string emailUser)
        {
            return new RequestQuest()
            {
                Title = "FOR THE EMPERIAR",
                Description = "Save the world",
                IsCompleted = false,
                EmailUser = emailUser
            };
        }
        private async Task<RequestUser> AddUser()
        {
            var user = CreateUser();
            var response = await _userService.AddendumUserAsync(user);
            return user;
        }
        private RequestUser CreateUser()
        {
            return new RequestUser()
            {
                Email = "test@test.test",
                Name = "Test User"
            };
        }
        public void Dispose()
        {
            _contextMock?.Dispose();
        }
    }
}
