using FleetControl.Application.Commands.Reservations.InsertReservation;
using FleetControl.Core.Entities;
using FleetControl.Core.Exceptions;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

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

            var repository = Substitute.For<IGenericRepository<Reservation>>();
            var projectRepository = Substitute.For<IGenericRepository<Project>>();
            var vehicleRepository = Substitute.For<IGenericRepository<FleetControl.Core.Entities.Vehicle>>();
            var driverRepository = Substitute.For<IGenericRepository<Driver>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.ProjectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            unitOfWork.VehicleRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((FleetControl.Core.Entities.Vehicle?)vehicle));
            unitOfWork.DriverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Insert] as InsertReservationCommand;

            var handler = new InsertReservationHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.ReservationRepository.Received().Create(Arg.Any<Reservation>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Insert_ThrowsBusinessException()
        {
            var repository = Substitute.For<IGenericRepository<Reservation>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.ReservationRepository.Returns(repository);

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

            await repository.DidNotReceive().Create(Arg.Any<Reservation>());
        }

        [Fact]
        public async Task DriverNotExists_Insert_Fail()
        {

            var project = _projectGenerator.Generate();
            var vehicle = _vehicleGenerator.Generate();

            var repository = Substitute.For<IGenericRepository<Reservation>>();
            var projectRepository = Substitute.For<IGenericRepository<Project>>();
            var vehicleRepository = Substitute.For<IGenericRepository<FleetControl.Core.Entities.Vehicle>>();
            var driverRepository = Substitute.For<IGenericRepository<Driver>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.ReservationRepository.Returns(repository);
            unitOfWork.ProjectRepository.Returns(projectRepository);
            unitOfWork.VehicleRepository.Returns(vehicleRepository);
            unitOfWork.DriverRepository.Returns(driverRepository);

            driverRepository.GetById(Arg.Any<int>()).ReturnsNull();
            projectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            vehicleRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((FleetControl.Core.Entities.Vehicle?)vehicle));

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Insert] as InsertReservationCommand;

            var handler = new InsertReservationHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            await repository.DidNotReceive().Create(Arg.Any<Reservation>());
        }

        [Fact]
        public async Task ProjectNotExists_Insert_Fail()
        {
            var driver = _driverGenerator.Generate();
            var vehicle = _vehicleGenerator.Generate();

            var repository = Substitute.For<IGenericRepository<Reservation>>();
            var projectRepository = Substitute.For<IGenericRepository<Project>>();
            var vehicleRepository = Substitute.For<IGenericRepository<FleetControl.Core.Entities.Vehicle>>();
            var driverRepository = Substitute.For<IGenericRepository<Driver>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.ReservationRepository.Returns(repository);
            unitOfWork.ProjectRepository.Returns(projectRepository);
            unitOfWork.VehicleRepository.Returns(vehicleRepository);
            unitOfWork.DriverRepository.Returns(driverRepository);

            projectRepository.GetById(Arg.Any<int>()).ReturnsNull();
            driverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));
            vehicleRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((FleetControl.Core.Entities.Vehicle?)vehicle));

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Insert] as InsertReservationCommand;

            var handler = new InsertReservationHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            await repository.DidNotReceive().Create(Arg.Any<Reservation>());
        }

        [Fact]
        public async Task VehicleNotExists_Insert_Fail()
        {
            var project = _projectGenerator.Generate();
            var driver = _driverGenerator.Generate();

            var repository = Substitute.For<IGenericRepository<Reservation>>();
            var projectRepository = Substitute.For<IGenericRepository<Project>>();
            var vehicleRepository = Substitute.For<IGenericRepository<FleetControl.Core.Entities.Vehicle>>();
            var driverRepository = Substitute.For<IGenericRepository<Driver>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.ReservationRepository.Returns(repository);
            unitOfWork.ProjectRepository.Returns(projectRepository);
            unitOfWork.VehicleRepository.Returns(vehicleRepository);
            unitOfWork.DriverRepository.Returns(driverRepository);

            projectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            driverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));
            vehicleRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((FleetControl.Core.Entities.Vehicle?)null));

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Insert] as InsertReservationCommand;

            var handler = new InsertReservationHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            await repository.DidNotReceive().Create(Arg.Any<Reservation>());
        }
    }
}
