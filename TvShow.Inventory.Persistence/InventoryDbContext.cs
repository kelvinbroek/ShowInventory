
using Microsoft.EntityFrameworkCore;
using TvShow.Inventory.Domain.Entities;

namespace TvShow.Inventory.Persistence
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }

        public DbSet<Show> Shows { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
