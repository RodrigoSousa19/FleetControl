using FleetControl.Application.Commands.Drivers.EnableDriver;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Drivers
{
    public class EnableDriverHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly DriverGenerator _entityGenerator = new DriverGenerator();

        [Fact]
        public async Task DriverExists_Enable_Success()
        {
            var driver = _entityGenerator.Generate();

            driver.Disable();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.DriverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));
            unitOfWork.DriverRepository.Update(Arg.Any<Driver>()).Returns(Task.CompletedTask);

            var handler = new EnableDriverHandler(unitOfWork);

            var command = _generatorsWork.DriverCommandsGenerator.Commands[CommandType.Enable] as EnableDriverCommand;


            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.DriverRepository.Received(1).Update(Arg.Any<Driver>());
        }

        [Fact]
        public async Task DriverNotExists_Enable_Fail()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.DriverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)null));

            var handler = new EnableDriverHandler(unitOfWork);

            var command = _generatorsWork.DriverCommandsGenerator.Commands[CommandType.Enable] as EnableDriverCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task DriverExistsAndIsEnabled_Enable_Fail()
        {
            var driver = _entityGenerator.Generate();
            driver.Enable();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.DriverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));
            unitOfWork.DriverRepository.Update(Arg.Any<Driver>()).Returns(Task.CompletedTask);

            var handler = new EnableDriverHandler(unitOfWork);

            var command = _generatorsWork.DriverCommandsGenerator.Commands[CommandType.Enable] as EnableDriverCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
