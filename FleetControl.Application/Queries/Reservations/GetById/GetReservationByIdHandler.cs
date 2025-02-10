using FleetControl.Application.Models;
using FleetControl.Application.Models.Reservations;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Queries.Reservations.GetById
{
    public class GetReservationByIdHandler : IRequestHandler<GetReservationByIdQuery, ResultViewModel<ReservationViewModel>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetReservationByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<ReservationViewModel>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetById(request.Id, includeNavigation: true, recursiveSearch: true);

            if (reservation is null)
                return ResultViewModel<ReservationViewModel>.Error("Não foi possível localizar a reserva informada.");

            var model = ReservationViewModel.FromEntity(reservation);

            return ResultViewModel<ReservationViewModel>.Success(model);
        }
    }
}
