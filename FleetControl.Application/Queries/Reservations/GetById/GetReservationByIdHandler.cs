using FleetControl.Application.Models;
using FleetControl.Application.Models.Reservations;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Queries.Reservations.GetById
{
    public class GetReservationByIdHandler : IRequestHandler<GetReservationByIdQuery, ResultViewModel<ReservationViewModel>>
    {

        private readonly IGenericRepository<Reservation> _repository;

        public GetReservationByIdHandler(IGenericRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<ReservationViewModel>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _repository.GetById(request.Id);

            if (reservation is null)
                return ResultViewModel<ReservationViewModel>.Error("Não foi possível localizar a reserva informada.");

            var model = ReservationViewModel.FromEntity(reservation);

            return ResultViewModel<ReservationViewModel>.Success(model);
        }
    }
}
