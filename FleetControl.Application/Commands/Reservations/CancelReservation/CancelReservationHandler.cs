using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.Reservation;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.CancelReservation
{
    public class CancelReservationHandler : IRequestHandler<CancelReservationCommand, ResultViewModel>
    {

        private readonly IGenericRepository<Reservation> _repository;

        public CancelReservationHandler(IGenericRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _repository.GetById(request.Id);

            if (reservation is null)
                return ResultViewModel.Error("Não foi possível encontrar a reserva especificada.");

            if (reservation.Status == ReservationStatus.Canceled || reservation.Status == ReservationStatus.Finished)
                return ResultViewModel.Error("O status atual da reserva não permite que ela seja cancelada.");

            reservation.CancelReservation();

            await _repository.Update(reservation);

            return ResultViewModel.Success();
        }
    }
}
