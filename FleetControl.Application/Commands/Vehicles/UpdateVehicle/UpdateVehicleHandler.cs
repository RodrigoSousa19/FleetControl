using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.UpdateVehicle
{
    public class UpdateVehicleHandler : IRequestHandler<UpdateVehicleCommand, ResultViewModel>
    {

        private readonly IUnitOfWork _unitOfWork;

        public UpdateVehicleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            new Validator().IsNotNullOrEmpty(request.Brand, ErrorsList.EmptyCarBrand)
                 .IsNotNullOrEmpty(request.Model, ErrorsList.EmptyCarModel)
                 .IsNotNullOrEmpty(request.Color, ErrorsList.EmptyCarColor)
                 .IsGreaterThanOrEqualTo(request.MileAge, 0, ErrorsList.InvalidCarMileAge)
                 .IsNotNullOrEmpty(request.FuelType, ErrorsList.EmptyCarFuelType)
                 .IsLicensePlateValid(request.LicensePlate, ErrorsList.InvalidLicensePlate)
                 .Validate();

            var vehicle = await _unitOfWork.VehicleRepository.GetById(request.IdVehicle);

            if (vehicle is null)
                return ResultViewModel.Error("Não foi possível encontrar o veículo informado.");

            vehicle.Update(request.Brand, request.Model, request.FuelType, request.LicensePlate, request.Color, request.MileAge);

            await _unitOfWork.VehicleRepository.Update(vehicle);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
