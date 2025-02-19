using FleetControl.Application.Commands.Reservations.UpdateReservation;
using FleetControl.Application.Commands.Vehicles;
using FleetControl.Core.Exceptions;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Vehicles.VehicleMaintenance
{
    public class UpdateVehicleMaintenanceHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly VehicleMaintenanceGenerator _maintenanceGenerator = new VehicleMaintenanceGenerator();
        [Fact]
        public async Task MaintenanceExists_Update_Success()
        {
            var maintenance = _maintenanceGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.VehicleMaintenanceRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((FleetControl.Core.Entities.VehicleMaintenance?)maintenance));

            unitOfWork.VehicleMaintenanceRepository.Update(Arg.Any<FleetControl.Core.Entities.VehicleMaintenance>()).Returns(Task.CompletedTask);

            var handler = new UpdateMaintenanceHandler(unitOfWork);

            var command = _generatorsWork.VehicleMaintenanceCommandsGenerator.Commands[CommandType.Update] as UpdateMaintenanceCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.VehicleMaintenanceRepository.Received(1).Update(Arg.Any<FleetControl.Core.Entities.VehicleMaintenance>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Update_Fail()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.VehicleMaintenanceRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((FleetControl.Core.Entities.VehicleMaintenance?)null));

            var handler = new UpdateReservationHandler(unitOfWork);

            var command = new UpdateReservationCommand
            {
                IdDriver = 0,
                EndDate = DateTime.MinValue,
                StartDate = DateTime.MaxValue,
                IdReservation = 0
            };

            await FluentActions.Invoking(() => handler.Handle(command, new CancellationToken()))
                .Should().ThrowAsync<BusinessException>();

            await unitOfWork.VehicleMaintenanceRepository.DidNotReceive().Update(Arg.Any<FleetControl.Core.Entities.VehicleMaintenance>());
        }
    }
}
