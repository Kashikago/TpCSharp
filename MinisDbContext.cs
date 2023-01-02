using Microsoft.EntityFrameworkCore;
using MinisAPI.Models;
namespace MinisAPI.Context
{
    public class MinisDbContext : DbContext
    {
        public DbSet<PaintItem> PaintItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseNpgsql("Host=localhost:5432;Database=MinisDB;Username=postgres;Password=ROOT");
        }
    }
}