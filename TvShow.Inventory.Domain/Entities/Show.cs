using System;
using System.Collections.Generic;
using TvShow.Inventory.Domain.Enums;

namespace TvShow.Inventory.Domain.Entities
{
    public class Show
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime Premiered { get; set; }

        public Language Language { get; set; }

        public string Summary { get; set; }

        #region Relations
        public IList<Genre> Genres { get; set; }


        #endregion
    }
}
