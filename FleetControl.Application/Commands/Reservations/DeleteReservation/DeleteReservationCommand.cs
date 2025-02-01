using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.DeleteReservation
{
    public class DeleteReservationCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public DeleteReservationCommand(int id)
        {
            Id = id;
        }
    }
}
