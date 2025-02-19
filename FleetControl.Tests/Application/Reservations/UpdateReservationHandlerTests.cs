using FleetControl.Application.Commands.Reservations.UpdateReservation;
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

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.DriverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));
            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));

            unitOfWork.ReservationRepository.Update(Arg.Any<Reservation>()).Returns(Task.CompletedTask);

            var handler = new UpdateReservationHandler(unitOfWork);

            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Update] as UpdateReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.ReservationRepository.Received(1).Update(Arg.Any<Reservation>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Update_Fail()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)null));

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

            await unitOfWork.ReservationRepository.DidNotReceive().Update(Arg.Any<Reservation>());
        }

        [Fact]
        public async Task ReservationExistsButDriverNotExists_Update_Fail()
        {
            var reservation = _reservationGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ReservationRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Reservation?)reservation));
            unitOfWork.DriverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)null));

            var handler = new UpdateReservationHandler(unitOfWork);
            var command = _generatorsWork.ReservationCommandsGenerator.Commands[CommandType.Update] as UpdateReservationCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
