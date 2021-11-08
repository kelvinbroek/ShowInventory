using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TvShow.Inventory.Application.Contracts.Repositories;
using TvShow.Inventory.Application.Responses;

namespace TvShow.Inventory.Application.Behaviour.Inventory.Queries.GetTvShowList
{
    public class GetTvShowListQuery : IRequest<ServiceResponse<IList<GetTvShowListVm>>>
    {
    }

    public class GetTvShowListQueryHandler : IRequestHandler<GetTvShowListQuery, ServiceResponse<IList<GetTvShowListVm>>>
    {
        private readonly ILogger _logger;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public GetTvShowListQueryHandler(IInventoryRepository inventoryRepository, IMapper mapper, ILogger<GetTvShowListQueryHandler> logger)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<IList<GetTvShowListVm>>> Handle(GetTvShowListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("GetTvShowListQueryHandler: Start execution");

                var showsFromInventory = await _inventoryRepository.GetAllAsync();
                var result = _mapper.Map<IList<GetTvShowListVm>>(showsFromInventory);

                _logger.LogInformation("GetTvShowListQueryHandler: Finished execution");

                return new ServiceResponse<IList<GetTvShowListVm>>(result);
            }
            catch(Exception e )
            {
                _logger.LogError($"GetTvShowListQueryHandler: Error during execution: {e.Message}");
                return new ServiceResponse<IList<GetTvShowListVm>>("An error occured while retrieving shows");
            }
        }
    }
}
