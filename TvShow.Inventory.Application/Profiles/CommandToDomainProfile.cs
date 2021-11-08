using AutoMapper;
using TvShow.Inventory.Application.Behaviour.Inventory.Commands;
using TvShow.Inventory.Domain.Entities;

namespace TvShow.Inventory.Application.Profiles
{
    public class CommandToDomainProfile : Profile
    {
        public CommandToDomainProfile()
        {
            CreateMap<AddTvShowVM, Show>().ReverseMap();
        }
    }
}
