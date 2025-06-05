using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestTrakingAPI.DataBase.DTO;
using QuestTrakingAPI.Services.Interfaces;

namespace QuestTrakingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestController : ControllerBase
    {
        private readonly IQuestServices _questServices;
        public QuestController(IQuestServices questServices)
        {
            _questServices = questServices;
        }

        [HttpPost("add-quest")]
        public async Task<IActionResult> AddQuest([FromBody] RequestQuest requestQuest)
        {
            var result = await _questServices.AddQuestAsync(requestQuest);
            if (result.Status == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetQuestsAll()
        {
            var result = await _questServices.GetQuestsAllAsync();
            if (result.Status == 404)
            {
                return NotFound(result);
            }
            return Ok(result);

        }

        [HttpGet("get-by-user-email/{Email}")]
        public async Task<IActionResult> GetQuestByUserEmail(string Email)
        {
            var result = await _questServices.GetQuestByUserEmailAsync(Email);
            if (result.Status == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpGet("get-by-id/{Id}")]
        public async Task<IActionResult> GetQuestById(int Id)
        {
            var result = await _questServices.GetQuestByIdAsync(Id);
            if (result.Status == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpDelete("delete-by-id/{Id}")]
        public async Task<IActionResult> DeleteQuestById(int Id)
        {
            var result = await _questServices.DeleteQuestByIdAsync(Id);
            if (result.Status == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPut("update-by-id/{Id}")]
        public async Task<IActionResult> UpdateQuestById(int Id, [FromBody] RequestQuest requestQuest)
        {
            var result = await _questServices.UpdateQuestByIdAsync(Id, requestQuest);
            if (result.Status == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
