using FleetControl.Application.Commands.Drivers.DeleteDriver;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace FleetControl.Tests.Application.Drivers
{
    public class DeleteDriverHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly DriverGenerator _entityGenerator = new DriverGenerator();

        [Fact]
        public async Task DriverExists_Delete_Success()
        {
            var driver = _entityGenerator.Generate();

            var repository = Substitute.For<IGenericRepository<Driver>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.DriverRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));
            repository.Update(Arg.Any<Driver>()).Returns(Task.CompletedTask);

            var handler = new DeleteDriverHandler(unitOfWork);

            var command = _generatorsWork.DriverCommandsGenerator.Commands[CommandType.Delete] as DeleteDriverCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Update(Arg.Any<Driver>());
        }

        [Fact]
        public async Task DriverNotExists_Delete_Failt()
        {
            var repository = Substitute.For<IGenericRepository<Driver>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.DriverRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).ReturnsNull();

            var handler = new DeleteDriverHandler(unitOfWork);

            var command = _generatorsWork.DriverCommandsGenerator.Commands[CommandType.Delete] as DeleteDriverCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
