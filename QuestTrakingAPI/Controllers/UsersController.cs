using Microsoft.AspNetCore.Mvc;
using QuestTrakingAPI.DataBase.DTO;

namespace QuestTrakingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpPost("add/")]
        public async Task<IActionResult> AddUsers([FromBody] RequestUser requestUser)
        {
            await Task.Delay(100);
            return Ok();
        }
        [HttpGet("get-all/")]
        public async Task<IActionResult> GetUsersAll()
        {
            await Task.Delay(100);
            return Ok();
        }
        [HttpGet("get-by-email/{Email}")]
        public async Task<IActionResult> GetUserByEmail(string Email)
        {
            await Task.Delay(100);
            return Ok();
        }
        [HttpDelete("delete-by-email/{Email}")]
        public async Task<IActionResult> DeleteUserByEmail(string Email)
        {
            await Task.Delay(100);
            return Ok();
        }

        [HttpPut("update-by-email/{Email}")]
        public async Task<IActionResult> UpdateUserByEmail(string Email, [FromBody] RequestUser requestUser)
        {
            await Task.Delay(100);
            return Ok();
        }
    }
}
