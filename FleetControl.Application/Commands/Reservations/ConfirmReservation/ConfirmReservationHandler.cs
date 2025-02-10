using FleetControl.Application.Models;
using FleetControl.Core.Enums.Reservation;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.ConfirmReservation
{
    public class ConfirmReservationHandler : IRequestHandler<ConfirmReservationCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmReservationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(ConfirmReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetById(request.Id);

            if (reservation is null)
                return ResultViewModel.Error("Não foi possível encontrar a reserva especificada.");

            if (reservation.Status != ReservationStatus.Pending)
                return ResultViewModel.Error("O status atual da reserva não permite que ela seja confirmada.");

            reservation.ConfirmReservation();

            await _unitOfWork.ReservationRepository.Update(reservation);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
