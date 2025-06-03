using Microsoft.AspNetCore.Mvc;
using QuestTrakingAPI.DataBase.DTO;
using QuestTrakingAPI.Response;
using QuestTrakingAPI.Services.Interfaces;

namespace QuestTrakingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _usersServices;
        public UsersController(IUserServices userServices)
        {
            _usersServices = userServices;
        }

        [HttpPost("add/")]
        public async Task<IActionResult> AddUsers([FromBody] RequestUser requestUser)
        {
            var response = await _usersServices.AddendumUserAsync(requestUser);
            if (response.Status==400)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("get-all/")]
        public async Task<IActionResult> GetUsersAll()
        {
            var response = await _usersServices.GetAllUserAsync();
            if (response.Status == 400)
            {
                return NotFound(response);
            }
            return Ok(response);
    
        }
        [HttpGet("get-by-email/{Email}")]
        public async Task<IActionResult> GetUserByEmail(string Email)
        {
            var response = await _usersServices.GetUserByEmailAsync(Email);
            if (response.Status == 400)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("delete-by-email/{Email}")]
        public async Task<IActionResult> DeleteUserByEmail(string Email)
        {
            var response = await _usersServices.DeleteUserByEmailAsync(Email);
            if (response.Status == 400)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut("update-by-email/{Email}")]
        public async Task<IActionResult> UpdateUserByEmail(string Email, [FromBody] RequestUser requestUser)
        {
            var response = await _usersServices.UpdateUserByEmailAsync(Email, requestUser);
            if (response.Status == 400)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
