using FleetControl.Application.Models;
using FleetControl.Application.Models.Reservations;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Queries.Reservations.GetAll
{
    public class GetAllReservationsHandler : IRequestHandler<GetAllReservationsQuery, ResultViewModel<IList<ReservationViewModel>>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetAllReservationsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<IList<ReservationViewModel>>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _unitOfWork.ReservationRepository.GetAll(includeNavigation: true,recursiveSearch: true);

            var model = reservations.Select(ReservationViewModel.FromEntity).ToList();

            return ResultViewModel<IList<ReservationViewModel>>.Success(model);
        }
    }
}
