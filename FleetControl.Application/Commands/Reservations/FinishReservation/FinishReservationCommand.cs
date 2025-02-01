using FleetControl.Application.Commands.Reservations.ConfirmReservation;
using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.FinishReservation
{
    public class FinishReservationCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public FinishReservationCommand(int id)
        {
            Id = id;
        }
    }
}
