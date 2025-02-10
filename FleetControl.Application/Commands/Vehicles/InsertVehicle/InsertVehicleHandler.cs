using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.InsertVehicle
{
    public class InsertVehicleHandler : IRequestHandler<InsertVehicleCommand, ResultViewModel<Vehicle>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public InsertVehicleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<Vehicle>> Handle(InsertVehicleCommand request, CancellationToken cancellationToken)
        {
            new Validator().IsNotNullOrEmpty(request.Brand, ErrorsList.EmptyCarBrand)
                .IsNotNullOrEmpty(request.Model, ErrorsList.EmptyCarModel)
                .IsNotNullOrEmpty(request.Color, ErrorsList.EmptyCarColor)
                .IsGreaterThanOrEqualTo(request.MileAge, 0, ErrorsList.InvalidCarMileAge)
                .IsNotNullOrEmpty(request.FuelType, ErrorsList.EmptyCarFuelType)
                .IsLicensePlateValid(request.LicensePlate, ErrorsList.InvalidLicensePlate)
                .Validate();

            var vehicle = await _unitOfWork.VehicleRepository.Create(request.ToEntity());

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel<Vehicle>.Success(vehicle);
        }
    }
}
