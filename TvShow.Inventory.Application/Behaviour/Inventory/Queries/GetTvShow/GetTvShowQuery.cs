using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TvShow.Inventory.Application.Contracts.Services;
using TvShow.Inventory.Application.Responses;

namespace TvShow.Inventory.Application.Behaviour.Inventory.Queries.GetTvShow
{
    public record GetTvShowQuery(string name, bool sortByPremieredDate) : IRequest<ServiceResponse<IList<GetTvShowVM>>>
    {
    }

    public class GetTvShowQueryHandler : IRequestHandler<GetTvShowQuery, ServiceResponse<IList<GetTvShowVM>>>
    {
        private readonly ILogger _logger;
        private readonly ITvMazeService _tvMazeService;

        public GetTvShowQueryHandler(ITvMazeService tvMazeService, ILogger<GetTvShowQueryHandler> logger)
        {
            _tvMazeService = tvMazeService;
            _logger = logger;
        }

        public async Task<ServiceResponse<IList<GetTvShowVM>>> Handle(GetTvShowQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("GetTvShowQueryHandler: Start execution");

                var showsFromTvMaze = await _tvMazeService.GetByNameAsync(request.name, request.sortByPremieredDate);

                _logger.LogInformation("GetTvShowQueryHandler: Finished execution");

                return new ServiceResponse<IList<GetTvShowVM>>(showsFromTvMaze);
            }
            catch (Exception e)
            {
                _logger.LogError($"GetTvShowQueryHandler: Error during execution: {e.Message}");
                return new ServiceResponse<IList<GetTvShowVM>>("An error occured while retrieving shows");
            }
        }
    }
}
