using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.UpdateReservation
{
    public class UpdateReservationHandler : IRequestHandler<UpdateReservationCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateReservationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            new Validator()
                .IsValidDateRange(request.StartDate, request.EndDate, ErrorsList.InvalidDateRange)
                .Validate();

            var reservation = await _unitOfWork.ReservationRepository.GetById(request.IdReservation);

            if (reservation is null)
                return ResultViewModel.Error("Não foi possível encontrar a reserva especificada.");

            var driver = await _unitOfWork.DriverRepository.GetById(request.IdDriver);
            if (driver is null)
                return ResultViewModel<Reservation>.Error("Não foi possível encontrar o motorista especificado.");

            reservation.Update(request.StartDate, request.EndDate, request.IdDriver);

            await _unitOfWork.ReservationRepository.Update(reservation);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
