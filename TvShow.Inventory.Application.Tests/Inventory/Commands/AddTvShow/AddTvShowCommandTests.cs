using Xunit;
using TvShow.Inventory.Application.Behaviour.Inventory.Commands;
using Moq;
using TvShow.Inventory.Application.Contracts.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TvShow.Inventory.Application.Profiles;
using System;
using FluentAssertions;
using TvShow.Inventory.Domain.Entities;
using System.Threading.Tasks;
using TvShow.Inventory.Application.Models;
using System.Collections.Generic;

namespace TvShow.Inventory.Application.Tests.Inventory.Commands.AddTvShow
{
    public class AddTvShowCommandTests
    {
        private Mock<IInventoryRepository> _inventoryRepository;
        private readonly IMock<ILogger<AddTvShowCommandHandler>> _logger;
        private readonly IMapper _mapper;

        public AddTvShowCommandTests()
        {
            _inventoryRepository = new Mock<IInventoryRepository>();
            _logger = new Mock<ILogger<AddTvShowCommandHandler>>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CommandToDomainProfile());
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task When_Given_Correct_object_Handler_should_return_success_response()
        {
            //Act
            var showObject = new AddTvShowVM { Name = "test", Language = 0, Premiered = DateTime.Now, Summary = "test" };
            var command = new AddTvShowCommand { TvShowDto = showObject };
            _inventoryRepository.Setup(x => x.CreateAsync(It.IsAny<Show>())).Returns(Task.CompletedTask);
            var handler = new AddTvShowCommandHandler(_inventoryRepository.Object, _logger.Object, _mapper);

            //Arrange
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task When_Given_Incorrect_object_Handler_should_return_errorMessages()
        {
            //Act
            var showObject = new AddTvShowVM { Name = "", Language = 0, Premiered = DateTime.Now, Summary = "test", Genres = new List<GenreDto> { new GenreDto { Name = "Action" } } };
            var command = new AddTvShowCommand { TvShowDto = showObject };
            var handler = new AddTvShowCommandHandler(_inventoryRepository.Object, _logger.Object, _mapper);

            //Arrange
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("'Name' must not be empty");
            result.Message.Should().Contain("Genre Id cannot be empty");
        }
    }
}
