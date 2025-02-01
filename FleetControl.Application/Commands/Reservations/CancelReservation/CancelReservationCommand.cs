using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.CancelReservation
{
    public class CancelReservationCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public CancelReservationCommand(int id)
        {
            Id = id;
        }
    }
}
