using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.DeleteReservation
{
    public class DeleteReservationHandler : IRequestHandler<DeleteReservationCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Reservation> _repository;

        public DeleteReservationHandler(IGenericRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _repository.GetById(request.Id);

            if (reservation is null)
                return ResultViewModel.Error("Não foi possível encontrar a reserva especificada.");

            reservation.SetAsDeleted();

            await _repository.Update(reservation);

            return ResultViewModel.Success();
        }
    }
}
