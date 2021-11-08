using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TvShow.Inventory.Application.Contracts.Repositories;
using TvShow.Inventory.Application.Responses;
using TvShow.Inventory.Domain.Entities;

namespace TvShow.Inventory.Application.Behaviour.Inventory.Commands
{
    public class AddTvShowCommand : IRequest<ServiceResponse<AddTvShowVM>>
    {
        public AddTvShowVM TvShowDto { get; set; }
    }

    public class AddTvShowCommandHandler : IRequestHandler<AddTvShowCommand, ServiceResponse<AddTvShowVM>>
    {
        private readonly ILogger _logger;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public AddTvShowCommandHandler(IInventoryRepository inventoryRepository, ILogger<AddTvShowCommandHandler> logger, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AddTvShowVM>> Handle(AddTvShowCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("AddTvShowCommandHandler: Start execution");

                var validator = new AddTvShowValidator();
                var validationResponse = await validator.ValidateAsync(request.TvShowDto);
                if (!validationResponse.IsValid)
                {
                    var errorResponse = "";
                    foreach (var error in validationResponse.Errors)
                    {
                        errorResponse += $"{error},";
                        _logger.LogWarning($"AddTvShowCommandHandler: Error while validating object: {error}");
                    }
                    return new ServiceResponse<AddTvShowVM>(errorResponse);
                }

                var show = _mapper.Map<Show>(request.TvShowDto);
                await _inventoryRepository.CreateAsync(show);

                _logger.LogInformation($"AddTvShowCommandHandler: Finished execution {request.TvShowDto.Name}");
                return new ServiceResponse<AddTvShowVM>($"Show with name {request.TvShowDto.Name} has been added to inventory", true);
            }
            catch(Exception e)
            {
                _logger.LogError($"AddTvShowCommandHandler: Error {e.Message}");
                return new ServiceResponse<AddTvShowVM>(e.Message);
            }
        }
    }
}
