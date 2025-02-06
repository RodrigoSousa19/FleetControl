using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.Vehicle;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class CancelMaintenanceHandler : IRequestHandler<CancelMaintenanceCommand, ResultViewModel>
    {
        private readonly IGenericRepository<VehicleMaintenance> _repository;
        private readonly IGenericRepository<Vehicle> _vehicleRepository;

        public CancelMaintenanceHandler(IGenericRepository<VehicleMaintenance> repository, IGenericRepository<Vehicle> vehicleRepository)
        {
            _repository = repository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ResultViewModel> Handle(CancelMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var maintenance = await _repository.GetById(request.Id);

            if (maintenance is not VehicleMaintenance { Status: MaintenanceStatus.Pending or MaintenanceStatus.InProgress })
                return ResultViewModel.Error("Não foi possível localizar a manutenção especificada ou o status não permite que a manutenção seja cancelada.");

            var vehicle = await _vehicleRepository.GetById(maintenance.IdVehicle);

            if (vehicle is not Vehicle { Status: VehicleStatus.InMaintenance })
                return ResultViewModel.Error("O status do veículo não permite que a manutenção seja finalizada");

            maintenance.CancelMaintenance();

            vehicle.SetAvailable();

            await _repository.Update(maintenance);

            await _vehicleRepository.Update(vehicle);

            return ResultViewModel.Success();

        }
    }
}
