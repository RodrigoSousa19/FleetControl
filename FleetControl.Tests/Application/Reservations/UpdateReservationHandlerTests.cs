using FleetControl.Application.Commands.Reservations.UpdateReservation;
using FleetControl.Core.Entities;
using FleetControl.Core.Exceptions;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Reservations
{
    public class UpdateReservationHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly ReservationGenerator _reservationGenerator = new ReservationGenerator();
        private readonly DriverGenerator _driverGenerator = new DriverGenerator();
        [Fact]
        public async Task ReservationExists_Update_Success()
        {
            var reservation = _reservationGenerator.Generate();
            var driver = _driverGenerator.Generate();

            var repository = Substitute.For<IGenericRepository<Reservation>>();
            var driverRepository = Substitute.For<IGenericRepository<Driver>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.ReservationRepository.Returns(repository);
            unitOfWork.DriverRepository.Returns(driverRepository);

            driverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));
            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));

            repository.Update(Arg.Any<Reservation>()).Returns(Task.CompletedTask);

            var handler = new UpdateReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Update] as UpdateReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Update(Arg.Any<Reservation>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Update_Fail()
        {
            var repository = Substitute.For<IGenericRepository<Reservation>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.ReservationRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)null));

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

            await repository.DidNotReceive().Update(Arg.Any<Reservation>());
        }

        [Fact]
        public async Task ReservationExistsButDriverNotExists_Update_Fail()
        {
            var reservation = _reservationGenerator.Generate();

            var repository = Substitute.For<IGenericRepository<Reservation>>();
            var driverRepository = Substitute.For<IGenericRepository<Driver>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.ReservationRepository.Returns(repository);
            unitOfWork.DriverRepository.Returns(driverRepository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));
            driverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)null));

            var handler = new UpdateReservationHandler(unitOfWork);
            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Update] as UpdateReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
