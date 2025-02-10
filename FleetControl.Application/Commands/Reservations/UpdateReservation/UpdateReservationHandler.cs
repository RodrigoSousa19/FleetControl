using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.UpdateReservation
{
    public class UpdateReservationHandler : IRequestHandler<UpdateReservationCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Reservation> _repository;

        public UpdateReservationHandler(IGenericRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            new Validator()
                .IsValidDateRange(request.StartDate, request.EndDate, ErrorsList.InvalidDateRange)
                .Validate();

            var reservation = await _repository.GetById(request.IdReservation);

            if (reservation is null)
                return ResultViewModel.Error("Não foi possível encontrar a reserva especificada.");

            reservation.Update(request.StartDate, request.EndDate, request.IdDriver);

            await _repository.Update(reservation);

            return ResultViewModel.Success();
        }
    }
}
