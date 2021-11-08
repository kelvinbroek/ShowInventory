using System.Collections.Generic;
using System.Threading.Tasks;
using TvShow.Inventory.Domain.Entities;

namespace TvShow.Inventory.Application.Contracts.Repositories
{
    public interface IInventoryRepository
    {
        Task<IList<Show>> GetAllAsync();
        Task CreateAsync(Show entity);
    }
}
