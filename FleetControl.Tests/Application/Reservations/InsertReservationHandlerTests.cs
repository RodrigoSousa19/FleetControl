using FleetControl.Application.Commands.Reservations.InsertReservation;
using FleetControl.Core.Entities;
using FleetControl.Core.Exceptions;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Reservations
{
    public class InsertReservationHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly ProjectGenerator _projectGenerator = new ProjectGenerator();
        private readonly DriverGenerator _driverGenerator = new DriverGenerator();
        private readonly VehicleGenerator _vehicleGenerator = new VehicleGenerator();

        [Fact]
        public async Task InputDataAreOk_Insert_Success()
        {
            var project = _projectGenerator.Generate();
            var driver = _driverGenerator.Generate();
            var vehicle = _vehicleGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ProjectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            unitOfWork.DriverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));
            unitOfWork.VehicleRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Vehicle?)vehicle));

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Insert] as InsertReservationCommand;

            var handler = new InsertReservationHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.ReservationRepository.Received(1).Create(Arg.Any<Reservation>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Insert_ThrowsBusinessException()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            var command = new InsertReservationCommand
            {
                EndDate = DateTime.MinValue,
                StartDate = DateTime.MaxValue,
                IdDriver = 0,
                IdProject = 0,
                IdVehicle = 0,
                Observation = ""
            };

            var handler = new InsertReservationHandler(unitOfWork);

            await FluentActions.Invoking(() => handler.Handle(command, new CancellationToken()))
                .Should().ThrowAsync<BusinessException>();

            await unitOfWork.ReservationRepository.DidNotReceive().Create(Arg.Any<Reservation>());
        }

        [Fact]
        public async Task DriverNotExists_Insert_Fail()
        {

            var project = _projectGenerator.Generate();
            var vehicle = _vehicleGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.DriverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)null));
            unitOfWork.ProjectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            unitOfWork.VehicleRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Vehicle?)vehicle));

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Insert] as InsertReservationCommand;

            var handler = new InsertReservationHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            await unitOfWork.ReservationRepository.DidNotReceive().Create(Arg.Any<Reservation>());
        }

        [Fact]
        public async Task ProjectNotExists_Insert_Fail()
        {
            var driver = _driverGenerator.Generate();
            var vehicle = _vehicleGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ProjectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null));

            unitOfWork.DriverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));
            unitOfWork.VehicleRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Vehicle?)vehicle));

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Insert] as InsertReservationCommand;

            var handler = new InsertReservationHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            await unitOfWork.ReservationRepository.DidNotReceive().Create(Arg.Any<Reservation>());
        }

        [Fact]
        public async Task VehicleNotExists_Insert_Fail()
        {
            var project = _projectGenerator.Generate();
            var driver = _driverGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ProjectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            unitOfWork.DriverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));
            unitOfWork.VehicleRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Vehicle?)null));

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Insert] as InsertReservationCommand;

            var handler = new InsertReservationHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            await unitOfWork.ReservationRepository.DidNotReceive().Create(Arg.Any<Reservation>());
        }
    }
}
