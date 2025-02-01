using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.UpdateReservation
{
    public class UpdateReservationCommand : IRequest<ResultViewModel>
    {
        public int IdReservation { get; set; }
        public int IdDriver { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
