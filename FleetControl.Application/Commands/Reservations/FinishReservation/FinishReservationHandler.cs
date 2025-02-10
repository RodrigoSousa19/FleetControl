using FleetControl.Application.Models;
using FleetControl.Core.Enums.Reservation;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.FinishReservation
{
    public class FinishReservationHandler : IRequestHandler<FinishReservationCommand, ResultViewModel>
    {

        private readonly IUnitOfWork _unitOfWork;

        public FinishReservationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(FinishReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetById(request.Id);

            if (reservation is null)
                return ResultViewModel.Error("Não foi possível encontrar a reserva especificada.");

            if (reservation.Status != ReservationStatus.Confirmed)
                return ResultViewModel.Error("O status atual da reserva não permite que ela seja finalizada.");

            reservation.FinishReservation();

            await _unitOfWork.ReservationRepository.Update(reservation);

            return ResultViewModel.Success();
        }
    }
}
