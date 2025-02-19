using FleetControl.Application.Commands.Drivers.DisableDriver;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Drivers
{
    public class DisableDriverHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly DriverGenerator _entityGenerator = new DriverGenerator();

        [Fact]
        public async Task DriverExists_Disable_Success()
        {
            var driver = _entityGenerator.Generate();

            driver.Enable();

            var repository = Substitute.For<IGenericRepository<Driver>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.DriverRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));
            repository.Update(Arg.Any<Driver>()).Returns(Task.CompletedTask);

            var handler = new DisableDriverHandler(unitOfWork);

            var command = _generatorsWork.DriverCommandsGenerator.Commands[CommandType.Disable] as DisableDriverCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Update(Arg.Any<Driver>());
        }

        [Fact]
        public async Task DriverNotExists_Disable_Fail()
        {
            var repository = Substitute.For<IGenericRepository<Driver>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.DriverRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)null));

            var handler = new DisableDriverHandler(unitOfWork);

            var command = _generatorsWork.DriverCommandsGenerator.Commands[CommandType.Disable] as DisableDriverCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task DriverExistsAndIsDisabled_Disable_Fail()
        {
            var driver = _entityGenerator.Generate();

            driver.Disable();

            var repository = Substitute.For<IGenericRepository<Driver>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.DriverRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));
            repository.Update(Arg.Any<Driver>()).Returns(Task.CompletedTask);

            var handler = new DisableDriverHandler(unitOfWork);

            var command = _generatorsWork.DriverCommandsGenerator.Commands[CommandType.Disable] as DisableDriverCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
