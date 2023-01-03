using Microsoft.EntityFrameworkCore;
using MinisAPI.Models;
namespace MinisAPI.Context
{
    public class MinisDbContext : DbContext
    {
        public DbSet<PaintItem>? PaintItems { get; set; }
        public MinisDbContext(DbContextOptions<MinisDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}