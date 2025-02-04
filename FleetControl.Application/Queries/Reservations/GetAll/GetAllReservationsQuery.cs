using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Queries.Reservations.GetAll
{
    public class GetAllReservationsQuery : IRequest<ResultViewModel<IList<Reservation>>>
    {
    }
}
