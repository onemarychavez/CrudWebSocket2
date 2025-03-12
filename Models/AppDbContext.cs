using Microsoft.EntityFrameworkCore;

namespace CrudWebSocket2.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<ClienteModels> Cliente { get; set; }
    }
}
