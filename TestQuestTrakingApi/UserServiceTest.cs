using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using QuestTrakingAPI.DataBase.DTO;
using QuestTrakingAPI.DataBase.Services;
using QuestTrakingAPI.Services.Realisation;

namespace TestQuestTrakingApi
{
    public class UserServiceTest : IDisposable
    {
        private readonly UserService _userService;
        private readonly Mock<ILogger<UserService>> _loggerMock;
        private readonly AppDBContext _contextMock;
        public UserServiceTest()
        {
            _loggerMock = new Mock<ILogger<UserService>>();
           var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _contextMock = new AppDBContext(options);
            _userService = new UserService(_loggerMock.Object, _contextMock);

            _contextMock.Database.EnsureDeleted();
            _contextMock.Database.EnsureCreated();


        }

        [Fact]
        public async Task TestAddUser()
        {
            var user = CreateUser();
            var response = await _userService.AddendumUserAsync(user);

            Assert.NotNull(response);
            Assert.Equal("User added successfully.", response.Message);
            Assert.Equal(200, response.Status);
           await _userService.DeleteUserByEmailAsync(user.Email);
        }
        [Fact]
        public async Task TestAddUserFail()
        {
            var user = CreateUserError();
            var response = await _userService.AddendumUserAsync(user);

            Assert.NotNull(response);
            Assert.Equal("Email and Name are required.", response.Message);
            Assert.Equal(400, response.Status);

        }

        [Fact]
        public async Task TestAddUserFail2()
        {
            var user = CreateUser();
            await _userService.AddendumUserAsync(user);
            var response = await _userService.AddendumUserAsync(user);

            Assert.NotNull(response);
            Assert.Equal("User with this email already exists.", response.Message);
            Assert.Equal(400, response.Status);
        }

        [Fact]
        public async Task TestRemoveUser()
        {
            var user = CreateUser();
            await _userService.AddendumUserAsync(user);
            var response = await _userService.DeleteUserByEmailAsync(user.Email);
            Assert.NotNull(response);
            Assert.Equal("User deleted successfully.", response.Message);


        }

        [Fact]
        public async Task TestRemoveUserFail()
        {
            var user = CreateUser();
            var response = await _userService.DeleteUserByEmailAsync(user.Email);
            Assert.NotNull(response);
            Assert.Equal("User not found.", response.Message);

        }

        [Fact]
        public async Task TestGetAllUserUser()
        {
            var user = CreateUser();

            await _userService.AddendumUserAsync(user);

            var response = await _userService.GetAllUserAsync();

            Assert.NotNull(response);
            Assert.Equal("Users retrieved successfully.", response.Message);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.First().Name, user.Name);
            Assert.Equal(response.Data.First().Email, user.Email);
            Assert.Single(response.Data);
            await _userService.DeleteUserByEmailAsync(user.Email);

        }

        [Fact]
        public async Task TestGetAllUserUserFail()
        {
            var user = CreateUser();
            await _userService.DeleteUserByEmailAsync(user.Email);
            var response = await _userService.GetAllUserAsync();

            Assert.NotNull(response);
            Assert.Equal("No users found.", response.Message);


        }

        [Fact]
        public async Task TestGetUserByEmailAsync()
        {
            var user = CreateUser();
            await _userService.AddendumUserAsync(user);
            var response = await _userService.GetUserByEmailAsync(user.Email);

            Assert.NotNull(response);
            Assert.Equal("User retrieved successfully.", response.Message);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Name, user.Name);
            Assert.Equal(response.Data.Email, user.Email);
            await _userService.DeleteUserByEmailAsync(user.Email);


        }
        [Fact]
        public async Task TestGetUserByEmailAsyncFail()
        {
            var user = CreateUser();

            var response = await _userService.GetUserByEmailAsync(user.Email);

            Assert.NotNull(response);
            Assert.Equal("User not found.", response.Message);

        }

        [Fact]
        public async Task UpdateUserByEmailAsync()
        {
            var user = CreateUser();
            var updateUser = UpdateUser();
            await _userService.AddendumUserAsync(user);
            var response = await _userService.UpdateUserByEmailAsync(user.Email, updateUser);
            Assert.NotNull(response);
            Assert.Equal("User updated successfully.", response.Message);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Name, updateUser.Name);
            Assert.Equal(response.Data.Email, updateUser.Email);

            await _userService.DeleteUserByEmailAsync(user.Email);

        }
        [Fact]
        public async Task UpdateUserByEmailAsyncFail()
        {
            var user = CreateUser();
            var updateUser = UpdateUser();

            var response = await _userService.UpdateUserByEmailAsync(user.Email, updateUser);
            Assert.NotNull(response);
            Assert.Equal("User not found.", response.Message);
 

  

        }

        private RequestUser CreateUser()
        {
            return new RequestUser()
            {
                Email = "test@test.test",
                Name = "Test User"
            };
        }
        private RequestUser UpdateUser()
        {
            return new RequestUser()
            {
                Email = "testupdate@testupdate.testupdate",
                Name = "Update Test User"
            };
        }
        private RequestUser CreateUserError()
        {
            return new RequestUser()
            {
                Email = "",
                Name = "Test User"
            };
        }

        public void Dispose()
        {
            _contextMock?.Dispose();
        }
    }
}
