using FleetControl.Application.Models;
using FleetControl.Application.Models.Reservations;
using MediatR;

namespace FleetControl.Application.Queries.Reservations.GetById
{

    public class GetReservationByIdQuery : IRequest<ResultViewModel<ReservationViewModel>>
    {
        public int Id { get; private set; }

        public GetReservationByIdQuery(int id)
        {
            Id = id;
        }
    }
}
