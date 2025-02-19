using FleetControl.Application.Commands.Vehicles.UpdateVehicle;
using FleetControl.Core.Entities;
using FleetControl.Core.Exceptions;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Vehicles
{
    public class UpdateVehicleHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly VehicleGenerator _entityGenerator = new VehicleGenerator();

        [Fact]
        public async Task VehicleExists_Update_Success()
        {
            var vehicle = _entityGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.VehicleRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Vehicle?)vehicle));
            unitOfWork.VehicleRepository.Update(Arg.Any<Vehicle>()).Returns(Task.CompletedTask);

            var handler = new UpdateVehicleHandler(unitOfWork);

            var command = _generatorsWork.VehicleCommandsGenerator.Commands[CommandType.Update] as UpdateVehicleCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.VehicleRepository.Received(1).Update(Arg.Any<Vehicle>());
        }

        [Fact]
        public async Task VehicleNotExists_Update_Fail()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.VehicleRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Vehicle?)null));

            var handler = new UpdateVehicleHandler(unitOfWork);

            var command = _generatorsWork.VehicleCommandsGenerator.Commands[CommandType.Update] as UpdateVehicleCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task InputDataAreNotOk_Update_Fail()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.VehicleRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Vehicle?)null));

            var handler = new UpdateVehicleHandler(unitOfWork);

            var command = new UpdateVehicleCommand()
            {
                Brand = "",
                Color = "",
                FuelType = "",
                IdVehicle = 0,
                LicensePlate = "",
                MileAge = -1,
                Model = ""
            };

            await FluentActions.Invoking(() => handler.Handle(command, new CancellationToken()))
                .Should().ThrowAsync<BusinessException>();

            await unitOfWork.VehicleRepository.DidNotReceive().Update(Arg.Any<Vehicle>());
        }
    }
}
