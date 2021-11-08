using System.Collections.Generic;
using System.Threading.Tasks;
using TvShow.Inventory.Application.Behaviour.Inventory.Queries.GetTvShow;

namespace TvShow.Inventory.Application.Contracts.Services
{
    public interface ITvMazeService
    {
        public Task<IList<GetTvShowVM>> GetByNameAsync(string name, bool sortByPremieredDate);
    }
}
