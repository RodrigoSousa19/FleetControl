using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.Vehicle;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class StartMaintenanceHandler : IRequestHandler<StartMaintenanceCommand, ResultViewModel>
    {
        private readonly IGenericRepository<VehicleMaintenance> _repository;
        private readonly IGenericRepository<Vehicle> _vehicleRepository;

        public StartMaintenanceHandler(IGenericRepository<VehicleMaintenance> repository, IGenericRepository<Vehicle> vehicleRepository)
        {
            _repository = repository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ResultViewModel> Handle(StartMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var maintenance = await _repository.GetById(request.Id);

            if (maintenance is not VehicleMaintenance { Status: MaintenanceStatus.Pending })
                return ResultViewModel.Error("Não foi possível localizar a manutenção especificada ou o status não permite que a manutenção seja iniciada.");

            var vehicle = await _vehicleRepository.GetById(maintenance.IdVehicle);

            if (vehicle is not Vehicle { Status: VehicleStatus.Available})
                return ResultViewModel.Error("A manutenção não pode ser iniciada neste veículo até que ele esteja disponível!");

            vehicle.SendToMaintenance();

            maintenance.SetInProgress();

            await _repository.Update(maintenance);

            await _vehicleRepository.Update(vehicle);

            return ResultViewModel.Success();
        }
    }
}
