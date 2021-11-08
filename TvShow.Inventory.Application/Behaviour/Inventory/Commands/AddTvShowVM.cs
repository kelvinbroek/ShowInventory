using System;
using System.Collections.Generic;
using TvShow.Inventory.Application.Models;
using TvShow.Inventory.Domain.Enums;

namespace TvShow.Inventory.Application.Behaviour.Inventory.Commands
{
    public class AddTvShowVM
    {
        public string Name { get; set; }

        public Language Language { get; set; }

        public DateTime Premiered { get; set; }

        public IList<GenreDto> Genres { get; set; }

        public string Summary { get; set; }
    }
}
