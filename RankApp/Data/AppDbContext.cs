using Microsoft.EntityFrameworkCore;

namespace RankApp.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<ItemModel> Items { get; set; } // Remove the nullable operator '?'

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=rankapp.db");
            }
        }
    }
}
