using FleetControl.Application.Models;
using FleetControl.Application.Models.Reservations;
using MediatR;

namespace FleetControl.Application.Queries.Reservations.GetAll
{
    public class GetAllReservationsQuery : IRequest<ResultViewModel<IList<ReservationViewModel>>>
    {
    }
}
