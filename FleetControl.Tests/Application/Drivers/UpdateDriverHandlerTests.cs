using FleetControl.Application.Commands.Drivers.UpdateDriver;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Drivers
{
    public class UpdateDriverHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly DriverGenerator _entityGenerator = new DriverGenerator();
        private readonly UserGenerator _userGenerator = new UserGenerator();

        [Fact]
        public async Task DriverExists_Update_Success()
        {
            var driver = _entityGenerator.Generate();
            var user = _userGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.DriverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));
            unitOfWork.UserRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((User?)user));
            unitOfWork.DriverRepository.Update(Arg.Any<Driver>()).Returns(Task.CompletedTask);

            var handler = new UpdateDriverHandler(unitOfWork);

            var command = _generatorsWork.DriverCommandsGenerator.Commands[CommandType.Update] as UpdateDriverCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.DriverRepository.Received(1).Update(Arg.Any<Driver>());
        }

        [Fact]
        public async Task DriverNotExists_Update_Fail()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.DriverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)null));
            unitOfWork.UserRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((User?)null));

            var handler = new UpdateDriverHandler(unitOfWork);

            var command = _generatorsWork.DriverCommandsGenerator.Commands[CommandType.Update] as UpdateDriverCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
