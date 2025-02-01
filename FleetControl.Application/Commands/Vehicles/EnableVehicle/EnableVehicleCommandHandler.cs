using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.EnableVehicle
{
    public class EnableVehicleCommandHandler : IRequestHandler<EnableVehicleCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Vehicle> _repository;

        public EnableVehicleCommandHandler(IGenericRepository<Vehicle> repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(EnableVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _repository.GetById(request.Id);

            if (vehicle is null)
                return ResultViewModel.Error("Não foi possível encontrar o veículo informado.");

            if (vehicle.Enabled)
                return ResultViewModel.Error("O veículo já se encontra ativo.");

            vehicle.Enable();

            await _repository.Update(vehicle);

            return ResultViewModel.Success();
        }
    }
}
