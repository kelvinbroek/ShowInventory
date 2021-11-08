using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TvShow.Inventory.Application.Contracts.Repositories;
using TvShow.Inventory.Domain.Entities;

namespace TvShow.Inventory.Persistence.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryDbContext _dbContext;

        public InventoryRepository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Show entity)
        {
            _dbContext.Shows.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Show>> GetAllAsync()
        {
            return await _dbContext.Shows.Include(x => x.Genres).AsNoTracking().ToListAsync();
        }
    }
}
