using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.ReservationsComments.DeleteReservationComment
{
    public class DeleteReservationCommentCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public DeleteReservationCommentCommand(int id)
        {
            Id = id;
        }
    }
}
