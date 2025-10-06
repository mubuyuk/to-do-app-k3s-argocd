using Microsoft.EntityFrameworkCore;
using todolistapi.Models;

namespace todolistapi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; } = default!;
    }
}
