
using FleetControl.Application.Commands.Vehicles;
using FleetControl.Core.Entities;
using FleetControl.Core.Exceptions;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.VehicleMaintenances.VehicleMaintenanceMaintenance
{
    public class InsertVehicleMaintenanceMaintenanceHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly VehicleGenerator _vehicleGenerator = new VehicleGenerator();

        [Fact]
        public async Task InputDataAreOk_Insert_Success()
        {
            var vehicle = _vehicleGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.VehicleRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Vehicle?)vehicle));

            var command = _generatorsWork.VehicleMaintenanceCommandsGenerator.Commands[CommandType.Insert] as InsertMaintenanceCommand;

            var handler = new InsertMaintenanceHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.VehicleMaintenanceRepository.Received(1).Create(Arg.Any<VehicleMaintenance>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Insert_ThrowsBusinessException()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            var command = new InsertMaintenanceCommand
            {
                Description = "",
                EndDate = DateTime.MinValue,
                StartDate = DateTime.MaxValue,
                IdVehicle = 0,
                TotalCost = 0
            };

            var handler = new InsertMaintenanceHandler(unitOfWork);

            await FluentActions.Invoking(() => handler.Handle(command, new CancellationToken()))
                .Should().ThrowAsync<BusinessException>();

            await unitOfWork.VehicleMaintenanceRepository.DidNotReceive().Create(new VehicleMaintenance(0, "", 0, DateTime.Now, DateTime.Now));
        }
    }
}
