using System;

namespace TvShow.Inventory.Infrastructure.Responses
{
    public class TvMazeShowResponse
    {
        public decimal Score { get; set; }

        public ShowJsonObject Show { get; set; }
    }

    public class ShowJsonObject
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Language { get; set; }

        public DateTime? Premiered { get; set; }

        public string[] Genres { get; set; }

        public string Summary { get; set; }
    }
}
