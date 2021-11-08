using System;
using TvShow.Inventory.Domain.Enums;

namespace TvShow.Inventory.Application.Behaviour.Inventory.Queries.GetTvShow
{
    public class GetTvShowVM
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Language { get; set; }

        public DateTime Premiered { get; set; }

        public string[] Genres { get; set; }

        public string Summary { get; set; }
    }
}
