using FleetControl.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
