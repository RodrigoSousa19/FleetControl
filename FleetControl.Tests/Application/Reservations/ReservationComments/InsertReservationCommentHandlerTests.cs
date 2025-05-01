using FleetControl.Application.Commands.Reservations.ReservationsComments;
using FleetControl.Core.Entities;
using FleetControl.Core.Exceptions;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Infrastructure.Persistence.Repositories.Interfaces;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

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

            var repository = Substitute.For<IGenericRepository<ReservationComment>>();
            var userRepository = Substitute.For<IUserRepository>();
            var reservationRepository = Substitute.For<IGenericRepository<Reservation>>();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ReservationCommentRepository.Returns(repository);
            unitOfWork.UserRepository.Returns(userRepository);
            unitOfWork.ReservationRepository.Returns(reservationRepository);

            userRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((User?)user));
            reservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));

            var command = _generatorsWork.ReservationCommentCommandsGenerator.Commands[CommandType.Insert] as InsertReservationCommentCommand;

            var handler = new InsertReservationCommentHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Create(Arg.Any<ReservationComment>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Insert_ThrowsBusinessException()
        {
            var repository = Substitute.For<IGenericRepository<ReservationComment>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ReservationCommentRepository.Returns(repository);

            var command = new InsertReservationCommentCommand
            {
                Content = "",
                IdReservation = 0,
                IdUser = 0
            };

            var handler = new InsertReservationCommentHandler(unitOfWork);

            await FluentActions.Invoking(() => handler.Handle(command, new CancellationToken()))
                .Should().ThrowAsync<BusinessException>();

            await repository.DidNotReceive().Create(Arg.Any<ReservationComment>());
        }

        [Fact]
        public async Task ReservationNotExists_Insert_Fail()
        {
            var user = _userGenerator.Generate();

            var repository = Substitute.For<IGenericRepository<ReservationComment>>();
            var userRepository = Substitute.For<IGenericRepository<User>>();
            var reservationRepository = Substitute.For<IGenericRepository<Reservation>>();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ReservationCommentRepository.Returns(repository);
            unitOfWork.ReservationRepository.Returns(reservationRepository);

            reservationRepository.GetById(Arg.Any<int>()).ReturnsNull();
            userRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((User?)user));

            var command = _generatorsWork.ReservationCommentCommandsGenerator.Commands[CommandType.Insert] as InsertReservationCommentCommand;

            var handler = new InsertReservationCommentHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            await repository.DidNotReceive().Create(Arg.Any<ReservationComment>());
        }

        [Fact]
        public async Task UserNotExists_Insert_Fail()
        {
            var reservation = _reservationGenerator.Generate();

            var repository = Substitute.For<IGenericRepository<ReservationComment>>();
            var userRepository = Substitute.For<IUserRepository>();
            var reservationRepository = Substitute.For<IGenericRepository<Reservation>>();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));

            var command = _generatorsWork.ReservationCommentCommandsGenerator.Commands[CommandType.Insert] as InsertReservationCommentCommand;

            var handler = new InsertReservationCommentHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            await repository.DidNotReceive().Create(Arg.Any<ReservationComment>());
        }
    }
}
