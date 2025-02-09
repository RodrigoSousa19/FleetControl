using FleetControl.Application.Models;
using FleetControl.Application.Models.Reservations;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Queries.Reservations.GetAll
{
    public class GetAllReservationsHandler : IRequestHandler<GetAllReservationsQuery, ResultViewModel<IList<ReservationViewModel>>>
    {

        private readonly IGenericRepository<Reservation> _repository;

        public GetAllReservationsHandler(IGenericRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<IList<ReservationViewModel>>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _repository.GetAll(includeNavigation: true, true);

            var model = reservations.Select(ReservationViewModel.FromEntity).ToList();

            return ResultViewModel<IList<ReservationViewModel>>.Success(model);
        }
    }
}
