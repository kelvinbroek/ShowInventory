using AutoMapper;
using TvShow.Inventory.Application.Behaviour.Inventory.Queries.GetTvShow;
using TvShow.Inventory.Application.Behaviour.Inventory.Queries.GetTvShowList;
using TvShow.Inventory.Application.Models;
using TvShow.Inventory.Domain.Entities;

namespace TvShow.Inventory.Application.Profiles
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<Show, GetTvShowVM>();
            CreateMap<Show, GetTvShowListVm>();


            CreateMap<Genre, GenreDto>().ReverseMap();
        }
    }
}
