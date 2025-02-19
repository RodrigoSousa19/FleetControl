using FleetControl.Application.Commands.Vehicles.InsertVehicle;
using FleetControl.Core.Entities;
using FleetControl.Core.Exceptions;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Vehicles
{
    public class InsertVehicleHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();

        [Fact]
        public async Task InputDataAreOk_Insert_Success()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            var command = _generatorsWork.VehicleCommandsGenerator.Commands[CommandType.Insert] as InsertVehicleCommand;

            var handler = new InsertVehicleHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.VehicleRepository.Received(1).Create(Arg.Any<Vehicle>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Insert_ThrowsBusinessException()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            var command = new InsertVehicleCommand
            {
                Brand = "",
                Color = "",
                FuelType = "",
                LicensePlate = "",
                MileAge = -1,
                Model = ""
            };

            var handler = new InsertVehicleHandler(unitOfWork);

            await FluentActions.Invoking(() => handler.Handle(command, new CancellationToken()))
                .Should().ThrowAsync<BusinessException>();

            await unitOfWork.VehicleRepository.DidNotReceive().Create(Arg.Any<Vehicle>());
        }
    }
}
