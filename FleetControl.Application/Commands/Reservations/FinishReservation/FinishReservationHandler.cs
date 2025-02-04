using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.Reservation;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.FinishReservation
{
    public class FinishReservationHandler : IRequestHandler<FinishReservationCommand, ResultViewModel>
    {

        private readonly IGenericRepository<Reservation> _repository;

        public FinishReservationHandler(IGenericRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(FinishReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _repository.GetById(request.Id);

            if (reservation is null)
                return ResultViewModel.Error("Não foi possível encontrar a reserva especificada.");

            if (reservation.Status != ReservationStatus.Confirmed)
                return ResultViewModel.Error("O status atual da reserva não permite que ela seja finalizada.");

            reservation.FinishReservation();

            await _repository.Update(reservation);

            return ResultViewModel.Success();
        }
    }
}
