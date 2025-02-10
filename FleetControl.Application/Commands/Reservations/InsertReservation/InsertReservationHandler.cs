using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.InsertReservation
{
    public class InsertReservationHandler : IRequestHandler<InsertReservationCommand, ResultViewModel<Reservation>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsertReservationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<Reservation>> Handle(InsertReservationCommand request, CancellationToken cancellationToken)
        {
            new Validator()
                .IsValidDateRange(request.StartDate, request.EndDate, ErrorsList.InvalidDateRange)
                .Validate();

            var driver = await _unitOfWork.DriverRepository.GetById(request.IdDriver);
            if (driver is null)
                return ResultViewModel<Reservation>.Error("Não foi possível encontrar o motorista especificado.");

            var vehicle = await _unitOfWork.VehicleRepository.GetById(request.IdVehicle);
            if (vehicle is null)
                return ResultViewModel<Reservation>.Error("Não foi possível encontrar o veículo especificado.");

            var project = await _unitOfWork.VehicleRepository.GetById(request.IdProject);
            if (project is null)
                return ResultViewModel<Reservation>.Error("Não foi possível encontrar o projeto especificado.");

            var reservation = await _unitOfWork.ReservationRepository.Create(request.ToEntity());

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel<Reservation>.Success(reservation);
        }
    }
}
