using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.Vehicle;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles.SendVehicleToMaintenance
{
    public class SendVehicleToMaintenanceHandler : IRequestHandler<SendVehicleToMaintenanceCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Vehicle> _repository;

        public SendVehicleToMaintenanceHandler(IGenericRepository<Vehicle> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(SendVehicleToMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _repository.GetById(request.Id);

            if (vehicle is null)
                return ResultViewModel.Error("Não foi possível encontrar o veiculo especificado");

            if (vehicle.Status != VehicleStatus.Available || vehicle.Status != VehicleStatus.Inactive)
                return ResultViewModel.Error("O status atual do veículo não permite que ele seja enviado para manutenção");

            vehicle.SendToMaintenance();

            await _repository.Update(vehicle);

            return ResultViewModel.Success();
        }
    }
}
