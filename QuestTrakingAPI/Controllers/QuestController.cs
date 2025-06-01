using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestTrakingAPI.DataBase.DTO;

namespace QuestTrakingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestController : ControllerBase
    {
        private readonly ILogger<QuestController> _logger;
        public QuestController(ILogger<QuestController> logger)
        {
            _logger = logger;
        }

        [HttpPost("add-quest")]
        public async Task<IActionResult> AddQuest([FromBody] RequestQuest requestQuest)
        {
            await Task.Delay(100);

            return Ok();
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetQuestsAll()
        {
            await Task.Delay(100);
            return Ok();
        }

        [HttpGet("get-by-user-email/{Email}")]
        public async Task<IActionResult> GetQuestByUserEmail(string Email)
        {
            await Task.Delay(100);
            return Ok();
        }
        [HttpGet("get-by-id/{Id}")]
        public async Task<IActionResult> GetQuestById(int Id)
        {
            await Task.Delay(100);
            return Ok();
        }

        [HttpDelete("delete-by-id/{Id}")]
        public async Task<IActionResult> DeleteQuestById(int Id)
        {
            await Task.Delay(100);
            return Ok();
        }

        [HttpPut("update-by-id/{Id}")]
        public async Task<IActionResult> UpdateQuestById(int Id, [FromBody] RequestQuest requestQuest)
        {
            await Task.Delay(100);
            return Ok();
        }
    }
}
