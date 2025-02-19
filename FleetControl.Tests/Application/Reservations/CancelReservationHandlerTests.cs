using FleetControl.Application.Commands.Reservations.ConfirmReservation;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.Reservation;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers.Generators;
using FleetControl.Tests.Helpers;
using MediatR;
using NSubstitute;
using FluentAssertions;
using FleetControl.Application.Commands.Reservations.CancelReservation;

namespace FleetControl.Tests.Application.Reservations
{
    public class CancelReservationHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly ReservationGenerator _entityGenerator = new ReservationGenerator();

        [Fact]
        public async Task ReservationExists_Cancel_Success()
        {
            var reservation = _entityGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));
            unitOfWork.ReservationRepository.Update(Arg.Any<Reservation>()).Returns(Task.CompletedTask);

            var handler = new CancelReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Cancel] as CancelReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.ReservationRepository.Received(1).Update(Arg.Any<Reservation>());

            reservation.Status.Should().Be(ReservationStatus.Canceled);
        }

        [Fact]
        public async Task ReservationNotExists_Cancel_Fail()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)null));

            var handler = new CancelReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Cancel] as CancelReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ReservationExistsAndIsCanceled_Cancel_Fail()
        {
            var reservation = _entityGenerator.Generate();

            reservation.CancelReservation();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));
            unitOfWork.ReservationRepository.Update(Arg.Any<Reservation>()).Returns(Task.CompletedTask);

            var handler = new CancelReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Cancel] as CancelReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            reservation.Status.Should().Be(ReservationStatus.Canceled);
        }

        [Fact]
        public async Task ReservationExistsAndIsFinished_Cancel_Fail()
        {
            var reservation = _entityGenerator.Generate();

            reservation.ConfirmReservation();
            reservation.FinishReservation();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));
            unitOfWork.ReservationRepository.Update(Arg.Any<Reservation>()).Returns(Task.CompletedTask);

            var handler = new CancelReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Cancel] as CancelReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            reservation.Status.Should().Be(ReservationStatus.Finished);
        }
    }
}
