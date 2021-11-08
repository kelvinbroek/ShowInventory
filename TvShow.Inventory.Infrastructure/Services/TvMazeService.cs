using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TvShow.Inventory.Application.Behaviour.Inventory.Queries.GetTvShow;
using TvShow.Inventory.Application.Contracts.Services;
using TvShow.Inventory.Infrastructure.Responses;
using TvShow.Inventory.Infrastructure.Settings;

namespace TvShow.Inventory.Infrastructure.Services
{
    public class TvMazeService : ITvMazeService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly IOptions<TvMazeSettings> _settings;


        public TvMazeService(HttpClient httpClient, IOptions<TvMazeSettings> settings, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _settings = settings;
        }

        public async Task<IList<GetTvShowVM>> GetByNameAsync(string name, bool sortByPremieredDate)
        {
            var jsonString = await _httpClient.GetStringAsync($"search/shows?q={name}");
            var tvMazeResponse = JsonConvert.DeserializeObject<IList<TvMazeShowResponse>>(jsonString);

            var premieredDate = DateTime.Parse(_settings.Value.MinimalPremieredDate);
            var showsPremiered = tvMazeResponse
                                    .Where(x => x.Show.Premiered >= premieredDate)
                                    .OrderBy(x => sortByPremieredDate ? x.Show.Premiered : DateTime.Now)
                                    .Select(x => x.Show)
                                    .ToList();

            var result = _mapper.Map<IList<GetTvShowVM>>(showsPremiered);

            return result;
        }
    }
}
