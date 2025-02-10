using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.Vehicle;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class StartMaintenanceHandler : IRequestHandler<StartMaintenanceCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public StartMaintenanceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(StartMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var maintenance = await _unitOfWork.VehicleMaintenanceRepository.GetById(request.Id);

            if (maintenance is not VehicleMaintenance { Status: MaintenanceStatus.Pending })
                return ResultViewModel.Error("Não foi possível localizar a manutenção especificada ou o status não permite que a manutenção seja iniciada.");

            var vehicle = await _unitOfWork.VehicleRepository.GetById(maintenance.IdVehicle);

            if (vehicle is not Vehicle { Status: VehicleStatus.Available })
                return ResultViewModel.Error("A manutenção não pode ser iniciada neste veículo até que ele esteja disponível!");

            vehicle.SendToMaintenance();

            maintenance.SetInProgress();

            await _unitOfWork.BeginTransactionAsync();
            await Task.WhenAll(
            _unitOfWork.VehicleMaintenanceRepository.Update(maintenance),
            _unitOfWork.VehicleRepository.Update(vehicle));
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return ResultViewModel.Success();
        }
    }
}
