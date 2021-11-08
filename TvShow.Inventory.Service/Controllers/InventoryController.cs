using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TvShow.Inventory.Application.Behaviour.Inventory.Commands;
using TvShow.Inventory.Application.Behaviour.Inventory.Queries.GetTvShow;
using TvShow.Inventory.Application.Behaviour.Inventory.Queries.GetTvShowList;

namespace TvShow.Inventory.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IList<GetTvShowListVm>>> Get()
        {
            var dtos = await _mediator.Send(new GetTvShowListQuery());
            return Ok(dtos);
        }

        [HttpGet("{name}/{sortByDate}")]
        public async Task<ActionResult<GetTvShowVM>> Get(string name, bool sortByDate)
        {
            var response = await _mediator.Send(new GetTvShowQuery(name, sortByDate));
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest($"Tv show with name {name} could not be found");
        }

        [HttpPost]
        public async Task<ActionResult<AddTvShowCommand>> Post([FromBody] AddTvShowVM model)
        {
            var response = await _mediator.Send(new AddTvShowCommand { TvShowDto = model });
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }
    }
}
