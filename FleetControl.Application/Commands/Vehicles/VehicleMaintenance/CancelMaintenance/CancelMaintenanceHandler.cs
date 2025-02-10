using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.Vehicle;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class CancelMaintenanceHandler : IRequestHandler<CancelMaintenanceCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CancelMaintenanceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(CancelMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var maintenance = await _unitOfWork.VehicleMaintenanceRepository.GetById(request.Id);

            if (maintenance is not VehicleMaintenance { Status: MaintenanceStatus.Pending or MaintenanceStatus.InProgress })
                return ResultViewModel.Error("Não foi possível localizar a manutenção especificada ou o status não permite que a manutenção seja cancelada.");

            var vehicle = await _unitOfWork.VehicleRepository.GetById(maintenance.IdVehicle);

            if (vehicle is not Vehicle { Status: VehicleStatus.InMaintenance })
                return ResultViewModel.Error("O status do veículo não permite que a manutenção seja finalizada");

            maintenance.CancelMaintenance();

            vehicle.SetAvailable();

            await _unitOfWork.BeginTransactionAsync();
            await Task.WhenAll(
                _unitOfWork.VehicleMaintenanceRepository.Update(maintenance),
                _unitOfWork.VehicleRepository.Update(vehicle)
            );
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return ResultViewModel.Success();

        }
    }
}
