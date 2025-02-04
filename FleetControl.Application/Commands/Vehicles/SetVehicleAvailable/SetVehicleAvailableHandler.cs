using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.Vehicle;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.SetVehicleAvailable
{
    public class SetVehicleAvailableHandler : IRequestHandler<SetVehicleAvailableCommand, ResultViewModel>
    {

        private readonly IGenericRepository<Vehicle> _repository;

        public SetVehicleAvailableHandler(IGenericRepository<Vehicle> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(SetVehicleAvailableCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _repository.GetById(request.Id);

            if (vehicle is null)
                return ResultViewModel.Error("Não foi possível encontrar o veiculo especificado");

            if (vehicle.Status == VehicleStatus.Available)
                return ResultViewModel.Error("O veículo já se encontra ativo e disponível.");

            vehicle.SetAvailable();

            await _repository.Update(vehicle);

            return ResultViewModel.Success();
        }
    }
}
