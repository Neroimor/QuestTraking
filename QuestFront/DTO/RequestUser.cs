using System.ComponentModel.DataAnnotations;

namespace QuestTrakingAPI.DataBase.DTO
{
    public class RequestUser
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
