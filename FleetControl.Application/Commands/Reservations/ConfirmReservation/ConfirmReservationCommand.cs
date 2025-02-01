using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.ConfirmReservation
{
    public class ConfirmReservationCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public ConfirmReservationCommand(int id)
        {
            Id = id;
        }
    }
}
