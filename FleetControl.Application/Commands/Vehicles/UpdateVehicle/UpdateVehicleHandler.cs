using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.UpdateVehicle
{
    public class UpdateVehicleHandler : IRequestHandler<UpdateVehicleCommand, ResultViewModel>
    {

        private readonly IGenericRepository<Vehicle> _repository;

        public UpdateVehicleHandler(IGenericRepository<Vehicle> repository)
        {
            _repository = repository;
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

            var vehicle = await _repository.GetById(request.IdVehicle);

            if (vehicle is null)
                return ResultViewModel.Error("Não foi possível encontrar o veículo informado.");

            vehicle.Update(request.Brand, request.Model, request.FuelType, request.LicensePlate, request.Color, request.MileAge);

            await _repository.Update(vehicle);

            return ResultViewModel.Success();
        }
    }
}
