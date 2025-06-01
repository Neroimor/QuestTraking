using Microsoft.EntityFrameworkCore;
using QuestTrakingAPI.DataBase.DTO;

namespace QuestTrakingAPI.DataBase.Services
{
    public class AppDBContext : DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Quest> Quests { get; set; }
    }
}
