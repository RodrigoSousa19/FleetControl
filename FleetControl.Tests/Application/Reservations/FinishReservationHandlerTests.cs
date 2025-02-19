﻿using FleetControl.Core.Entities;
using FleetControl.Core.Enums.Reservation;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers.Generators;
using FleetControl.Tests.Helpers;
using MediatR;
using NSubstitute;
using FluentAssertions;
using FleetControl.Application.Commands.Reservations.FinishReservation;

namespace FleetControl.Tests.Application.Reservations
{
    public class FinishReservationHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly ReservationGenerator _entityGenerator = new ReservationGenerator();

        [Fact]
        public async Task ReservationExists_Finish_Success()
        {
            var reservation = _entityGenerator.Generate();

            reservation.ConfirmReservation();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));
            unitOfWork.ReservationRepository.Update(Arg.Any<Reservation>()).Returns(Task.CompletedTask);

            var handler = new FinishReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Finish] as FinishReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.ReservationRepository.Received(1).Update(Arg.Any<Reservation>());

            reservation.Status.Should().Be(ReservationStatus.Finished);
        }

        [Fact]
        public async Task ReservationNotExists_Finish_Fail()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)null));

            var handler = new FinishReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Finish] as FinishReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ReservationExistsAndIsPending_Finish_Fail()
        {
            var reservation = _entityGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));
            unitOfWork.ReservationRepository.Update(Arg.Any<Reservation>()).Returns(Task.CompletedTask);

            var handler = new FinishReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Finish] as FinishReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            reservation.Status.Should().Be(ReservationStatus.Pending);
        }

        [Fact]
        public async Task ReservationExistsAndIsCanceled_Finish_Fail()
        {
            var reservation = _entityGenerator.Generate();

            reservation.CancelReservation();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));
            unitOfWork.ReservationRepository.Update(Arg.Any<Reservation>()).Returns(Task.CompletedTask);

            var handler = new FinishReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Finish] as FinishReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            reservation.Status.Should().Be(ReservationStatus.Canceled);
        }

        [Fact]
        public async Task ReservationExistsAndIsFinished_Finish_Fail()
        {
            var reservation = _entityGenerator.Generate();

            reservation.ConfirmReservation();
            reservation.FinishReservation();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));
            unitOfWork.ReservationRepository.Update(Arg.Any<Reservation>()).Returns(Task.CompletedTask);

            var handler = new FinishReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Finish] as FinishReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            reservation.Status.Should().Be(ReservationStatus.Finished);
        }
    }
}
