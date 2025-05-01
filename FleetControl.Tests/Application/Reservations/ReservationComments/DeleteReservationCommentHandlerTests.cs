using FleetControl.Application.Commands.Reservations.ReservationsComments;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace FleetControl.Tests.Application.Reservations.ReservationComments
{
    public class DeleteReservationCommentHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly ReservationCommentGenerator _entityGenerator = new ReservationCommentGenerator();

        [Fact]
        public async Task ReservationCommentExists_Delete_Success()
        {
            var reservationComment = _entityGenerator.Generate();

            var repository = Substitute.For<IGenericRepository<ReservationComment>>();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ReservationCommentRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((ReservationComment?)reservationComment));
            repository.Update(Arg.Any<ReservationComment>()).Returns(Task.CompletedTask);

            var handler = new DeleteReservationCommentHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommentCommandsGenerator.Commands[CommandType.Delete] as DeleteReservationCommentCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.ReservationCommentRepository.Received(1).Update(Arg.Any<ReservationComment>());
        }

        [Fact]
        public async Task ReservationCommentNotExists_Delete_Failt()
        {

            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.ReservationCommentRepository.GetById(Arg.Any<int>()).ReturnsNull();

            var handler = new DeleteReservationCommentHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommentCommandsGenerator.Commands[CommandType.Delete] as DeleteReservationCommentCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
