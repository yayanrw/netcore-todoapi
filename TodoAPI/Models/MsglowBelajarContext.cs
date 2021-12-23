using Microsoft.EntityFrameworkCore;

namespace TodoAPI.Models
{
    public class MsglowBelajarContext : DbContext
    {
        public MsglowBelajarContext(DbContextOptions options) : base(options)
        {}

        public DbSet<TodoModel> Todos { get; set; }
        public DbSet<MemberModel> Member { get; set; }
    }
}
