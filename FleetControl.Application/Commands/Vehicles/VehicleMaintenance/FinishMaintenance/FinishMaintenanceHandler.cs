using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.Vehicle;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Vehicles
{
    public class FinishMaintenanceHandler : IRequestHandler<FinishMaintenanceCommand, ResultViewModel>
    {
        private readonly IGenericRepository<VehicleMaintenance> _repository;

        public FinishMaintenanceHandler(IGenericRepository<VehicleMaintenance> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(FinishMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var maintenance = await _repository.GetById(request.Id);

            if (maintenance is VehicleMaintenance { Status: MaintenanceStatus.InProgress })
            {
                maintenance.FinishMaintenance();

                await _repository.Update(maintenance);

                return ResultViewModel.Success();
            }

            return ResultViewModel.Error("Não foi possível localizar a manutenção especificada ou o status não permite que a manutenção seja finalizada.");
        }
    }
}
