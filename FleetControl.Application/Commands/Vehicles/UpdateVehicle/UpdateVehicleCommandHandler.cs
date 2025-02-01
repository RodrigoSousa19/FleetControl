using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.UpdateVehicle
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, ResultViewModel>
    {

        private readonly IGenericRepository<Vehicle> _repository;

        public UpdateVehicleCommandHandler(IGenericRepository<Vehicle> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _repository.GetById(request.IdVehicle);

            if (vehicle is null)
                return ResultViewModel.Error("Não foi possível encontrar o veículo informado.");

            vehicle.Update(request.Brand, request.Model, request.FuelType, request.LicensePlate, request.Color, request.MileAge);

            await _repository.Update(vehicle);

            return ResultViewModel.Success();
        }
    }
}
