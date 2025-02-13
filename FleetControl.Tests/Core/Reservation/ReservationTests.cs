using FleetControl.Core.Enums.Reservation;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;

namespace FleetControl.Tests.Core.Reservation
{
    public class ReservationTests
    {
        private readonly ReservationGenerator _generator = new ReservationGenerator();

        [Fact]
        public void ReservationIsCreated_SetDeleted_Success()
        {
            var reservation = _generator.Generate();

            reservation.SetAsDeleted();

            reservation.IsDeleted.Should().BeTrue();
        }

        [Fact]
        public void ReservationIsCreated_Confirm_Success()
        {
            var reservation = _generator.Generate();

            reservation.ConfirmReservation();

            reservation.Status.Should().Be(ReservationStatus.Confirmed);
        }

        [Fact]
        public void ReservationIsCreated_Cancel_Success()
        {
            var reservation = _generator.Generate();

            reservation.CancelReservation();

            reservation.Status.Should().Be(ReservationStatus.Canceled);
        }

        [Fact]
        public void ReservationIsCreated_Finish_StatusDontChange()
        {
            var reservation = _generator.Generate();

            reservation.FinishReservation();

            reservation.Status.Should().Be(ReservationStatus.Pending);
        }

        [Fact]
        public void ReservationIsConfirmed_Cancel_Success()
        {
            var reservation = _generator.Generate();

            reservation.ConfirmReservation();

            reservation.CancelReservation();

            reservation.Status.Should().Be(ReservationStatus.Canceled);
        }

        [Fact]
        public void ReservationIsConfirmed_Finish_Success()
        {
            var reservation = _generator.Generate();

            reservation.ConfirmReservation();

            reservation.FinishReservation();

            reservation.Status.Should().Be(ReservationStatus.Finished);
        }

        [Fact]
        public void ReservationIsFinished_Cancel_StatusDontChange()
        {
            var reservation = _generator.Generate();

            reservation.ConfirmReservation();

            reservation.FinishReservation();

            reservation.CancelReservation();

            reservation.Status.Should().Be(ReservationStatus.Finished);
        }

        [Fact]
        public void ReservationIsCanceled_Start_StatusDontChange()
        {
            var reservation = _generator.Generate();

            reservation.CancelReservation();

            reservation.ConfirmReservation();

            reservation.Status.Should().Be(ReservationStatus.Canceled);
        }
    }
}
