using FleetControl.Application.Commands.Reservations.ReservationsComments;
using FleetControl.Core.Entities;
using FleetControl.Core.Exceptions;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Reservations.ReservationComments
{
    public class InsertReservationCommentHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly UserGenerator _userGenerator = new UserGenerator();
        private readonly ReservationGenerator _reservationGenerator = new ReservationGenerator();

        [Fact]
        public async Task InputDataAreOk_Insert_Success()
        {
            var user = _userGenerator.Generate();
            var reservation = _reservationGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.UserRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((User?)user));
            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));

            var command = _generatorsWork.ReservationCommentCommandsGenerator.Commands[CommandType.Insert] as InsertReservationCommentCommand;

            var handler = new InsertReservationCommentHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.ReservationCommentRepository.Received(1).Create(Arg.Any<ReservationComment>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Insert_ThrowsBusinessException()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            var command = new InsertReservationCommentCommand
            {
                Content = "",
                IdReservation = 0,
                IdUser = 0
            };

            var handler = new InsertReservationCommentHandler(unitOfWork);

            await FluentActions.Invoking(() => handler.Handle(command, new CancellationToken()))
                .Should().ThrowAsync<BusinessException>();

            await unitOfWork.ReservationCommentRepository.DidNotReceive().Create(Arg.Any<ReservationComment>());
        }

        [Fact]
        public async Task ReservationNotExists_Insert_Fail()
        {
            var user = _userGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)null));
            unitOfWork.UserRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((User?)user));

            var command = _generatorsWork.ReservationCommentCommandsGenerator.Commands[CommandType.Insert] as InsertReservationCommentCommand;

            var handler = new InsertReservationCommentHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            await unitOfWork.ReservationCommentRepository.DidNotReceive().Create(Arg.Any<ReservationComment>());
        }

        [Fact]
        public async Task UserNotExists_Insert_Fail()
        {
            var reservation = _reservationGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));
            unitOfWork.UserRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((User?)null));

            var command = _generatorsWork.ReservationCommentCommandsGenerator.Commands[CommandType.Insert] as InsertReservationCommentCommand;

            var handler = new InsertReservationCommentHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            await unitOfWork.ReservationCommentRepository.DidNotReceive().Create(Arg.Any<ReservationComment>());
        }
    }
}
