using AutoMapper;
using TvShow.Inventory.Application.Behaviour.Inventory.Queries.GetTvShow;
using TvShow.Inventory.Infrastructure.Responses;

namespace TvShow.Inventory.Infrastructure
{
    public class ResponseToDtoProfile : Profile
    {
        public ResponseToDtoProfile()
        {
            CreateMap<ShowJsonObject, GetTvShowVM>();
        }
    }
}
