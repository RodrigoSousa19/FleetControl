using FleetControl.Application.Commands.Reservations.ConfirmReservation;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.Reservation;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Reservations
{
    public class ConfirmReservationHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly ReservationGenerator _entityGenerator = new ReservationGenerator();

        [Fact]
        public async Task ReservationExists_Confirm_Success()
        {
            var reservation = _entityGenerator.Generate();

            reservation.Disable();

            var repository = Substitute.For<IGenericRepository<Reservation>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ReservationRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));
            repository.Update(Arg.Any<Reservation>()).Returns(Task.CompletedTask);

            var handler = new ConfirmReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Confirm] as ConfirmReservationCommand;


            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Update(Arg.Any<Reservation>());

            reservation.Status.Should().Be(ReservationStatus.Confirmed);
        }

        [Fact]
        public async Task ReservationNotExists_Confirm_Fail()
        {
            var repository = Substitute.For<IGenericRepository<Reservation>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ReservationRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)null));

            var handler = new ConfirmReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Confirm] as ConfirmReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ReservationExistsAndIsConfirmed_Confirm_Fail()
        {
            var reservation = _entityGenerator.Generate();

            reservation.ConfirmReservation();

            var repository = Substitute.For<IGenericRepository<Reservation>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ReservationRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));
            repository.Update(Arg.Any<Reservation>()).Returns(Task.CompletedTask);

            var handler = new ConfirmReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Confirm] as ConfirmReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            reservation.Status.Should().Be(ReservationStatus.Confirmed);
        }
    }
}
