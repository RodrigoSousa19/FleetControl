using FleetControl.Application.Commands.Reservations.DeleteReservation;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers.Generators;
using FleetControl.Tests.Helpers;
using MediatR;
using NSubstitute;
using FluentAssertions;
using FleetControl.Core.Interfaces.Generic;

namespace FleetControl.Tests.Application.Reservations
{
    public class DeleteReservationHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly ReservationGenerator _entityGenerator = new ReservationGenerator();

        [Fact]
        public async Task ReservationExists_Delete_Success()
        {
            var reservation = _entityGenerator.Generate();

            var repository = Substitute.For<IGenericRepository<Reservation>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ReservationRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));
            repository.Update(Arg.Any<Reservation>()).Returns(Task.CompletedTask);

            var handler = new DeleteReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Delete] as DeleteReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Update(Arg.Any<Reservation>());
        }

        [Fact]
        public async Task ReservationNotExists_Delete_Failt()
        {
            var repository = Substitute.For<IGenericRepository<Reservation>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ReservationRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)null));

            var handler = new DeleteReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Delete] as DeleteReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
