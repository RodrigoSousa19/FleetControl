using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.DisableVehicle
{
    public class DisableVehicleCommandHandler : IRequestHandler<DisableVehicleCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Vehicle> _repository;

        public DisableVehicleCommandHandler(IGenericRepository<Vehicle> repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(DisableVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _repository.GetById(request.Id);

            if (vehicle is null)
                return ResultViewModel.Error("Não foi possível encontrar o veículo informado.");

            if (!vehicle.Enabled)
                return ResultViewModel.Error("O veículo já se encontra inativo.");

            vehicle.Disable();

            await _repository.Update(vehicle);

            return ResultViewModel.Success();
        }
    }
}
