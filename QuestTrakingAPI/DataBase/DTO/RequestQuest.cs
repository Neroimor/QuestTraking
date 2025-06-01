using System.ComponentModel.DataAnnotations;

namespace QuestTrakingAPI.DataBase.DTO
{
    public class RequestQuest
    {
        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        [Required, EmailAddress]
        public string EmailUser { get; set; } = string.Empty;
    }
}
