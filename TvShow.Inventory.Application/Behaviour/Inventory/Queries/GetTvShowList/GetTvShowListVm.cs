using System;
using TvShow.Inventory.Domain.Enums;

namespace TvShow.Inventory.Application.Behaviour.Inventory.Queries.GetTvShowList
{
    public class GetTvShowListVm
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public Language Language { get; set; }

        public DateTime Premiered { get; set; }
    }
}
